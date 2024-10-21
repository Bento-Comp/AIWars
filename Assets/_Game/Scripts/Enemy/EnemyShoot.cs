using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public System.Action OnShootCooldownOver;
    public System.Action OnShoot;


    [SerializeField]
    private EnemyState m_enemyState = null;

    [SerializeField]
    private EnemyAim m_enemyAim = null;

    [SerializeField]
    private GameObject m_projectilePrefab = null;

    [SerializeField]
    private GameObject m_projectileSpawnPosition = null;

    [SerializeField]
    private ParticleSystem m_fireFxReference = null;

    [SerializeField]
    private float m_chargingShootTime = 0.75f;

    [SerializeField]
    private float m_shootCooldown = 1.5f;


    private Coroutine m_shootCooldownCoroutine;
    private float m_timer;
    private bool m_canChargeShoot;
    private bool m_isEnabled;


    private void OnEnable()
    {
        m_enemyAim.OnLockAimDirection += OnLockAimDirection;

        m_enemyState.OnInitialize += OnInitialize;
        m_enemyState.OnDisableEnemy += OnDisableEnemy;
    }

    private void OnDisable()
    {
        m_enemyAim.OnLockAimDirection -= OnLockAimDirection;

        m_enemyState.OnInitialize -= OnInitialize;
        m_enemyState.OnDisableEnemy -= OnDisableEnemy;
    }

    private void Update()
    {
        ChargeShoot();
    }


    private void OnInitialize()
    {
        m_isEnabled = true;
        m_canChargeShoot = false;
    }


    private void OnDisableEnemy()
    {
        m_isEnabled = false;
    }


    private void ChargeShoot()
    {
        if (m_isEnabled == false)
            return;

        if (m_canChargeShoot)
        {
            m_timer += Time.deltaTime;

            if(m_timer > m_chargingShootTime)
            {
                Shoot();
            }
        }
        else
        {
            m_timer = 0;
        }
    }


    private void Shoot()
    {
        m_canChargeShoot = false;

        GameObject instantiatedProjectile = Instantiate(m_projectilePrefab, m_projectileSpawnPosition.transform.position, Quaternion.identity);

        Projectile projectileReference = instantiatedProjectile.GetComponent<Projectile>();

        EnableShootFx();

        if (projectileReference != null)
        {
            Vector3 projectileDirection = m_projectileSpawnPosition.transform.forward;

            projectileDirection.y = 0f;

            projectileDirection.Normalize();

            instantiatedProjectile.transform.forward = projectileDirection;

            projectileReference.Initialize(m_projectileSpawnPosition.transform.position, projectileDirection, 3f, 1f);
        }

        StartShootCooldown();

        OnShoot?.Invoke();
    }


    private void EnableShootFx()
    {
        DisableShootFx();

        m_fireFxReference.Play();
    }

    private void DisableShootFx()
    {
        if (m_fireFxReference.isPlaying)
            m_fireFxReference.Stop();
    }

    private void StartShootCooldown()
    {
        if (m_shootCooldownCoroutine != null)
            StopCoroutine(m_shootCooldownCoroutine);

        m_shootCooldownCoroutine = StartCoroutine(ShootCooldownCoroutine());
    }


    private IEnumerator ShootCooldownCoroutine()
    {
        yield return new WaitForSeconds(m_shootCooldown);
        OnShootCooldownOver?.Invoke();
    }


    private void OnLockAimDirection()
    {
        m_canChargeShoot = true;
    }

}
