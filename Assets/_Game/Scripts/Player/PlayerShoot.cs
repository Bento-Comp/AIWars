using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public static System.Action OnPlayerShoot;

    [SerializeField]
    private PlayerDetector m_playerDetector = null;

    [SerializeField]
    private GameObject m_projectilePrefab = null;

    [SerializeField]
    private GameObject m_projectileSpawnPosition = null;

    [SerializeField]
    private ParticleSystem m_fireFxReference = null;

    [SerializeField]
    private PlayerStat_ShootDamage m_shootDamageStat = null;

    [SerializeField]
    private PlayerStat_ShootSpeed m_shootSpeedStat = null;


    private Coroutine m_shootCooldownCoroutine;
    private float m_shootSpeed = 2f;
    private float m_damage = 3.5f;
    private bool m_canWeaponFire;
    private bool m_isAllowedToShoot;


    private void OnEnable()
    {
        m_shootDamageStat.OnStatChange += OnDamageStatChange;
        m_shootSpeedStat.OnStatChange += OnShootSpeedStatChange;

        PlayerStateController.OnPlayerAlive += Initialize;
    }

    private void OnDisable()
    {
        m_shootDamageStat.OnStatChange -= OnDamageStatChange;
        m_shootSpeedStat.OnStatChange -= OnShootSpeedStatChange;

        PlayerStateController.OnPlayerAlive -= Initialize;
    }


    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        Shoot();
    }


    private void OnDamageStatChange()
    {
        m_damage = m_shootDamageStat.GetStatValue();
    }

    private void OnShootSpeedStatChange()
    {
        m_shootSpeed = m_shootSpeedStat.GetStatValue();
    }

    private void Initialize()
    {
        EnableShoot();
        m_canWeaponFire = true;
    }

    private void EnableShoot()
    {
        m_isAllowedToShoot = true;
    }

    private void DisableShoot()
    {
        m_isAllowedToShoot = false;
    }

    private void Shoot()
    {
        if (m_isAllowedToShoot && m_playerDetector.NearestObject != null)
        {
            if (m_canWeaponFire)
            {
                EnableShootFx();

                GameObject instantiatedProjectile = Instantiate(m_projectilePrefab, m_projectileSpawnPosition.transform.position, Quaternion.identity);

                Projectile projectileReference = instantiatedProjectile.GetComponent<Projectile>();

                if (projectileReference != null)
                {
                    Vector3 projectileDirection = (m_playerDetector.NearestObject.transform.position - m_projectileSpawnPosition.transform.position);

                    projectileDirection.y = 0f;

                    projectileDirection.Normalize();

                    instantiatedProjectile.transform.forward = projectileDirection;

                    projectileReference.Initialize(m_projectileSpawnPosition.transform.position, projectileDirection, 3f, m_damage);

                    OnPlayerShoot?.Invoke();
                }

                StartShootCooldown();
            }
        }
        else
        {
            DisableShootFx();
        }
    }


    private void StartShootCooldown()
    {
        m_canWeaponFire = false;

        if (m_shootCooldownCoroutine != null)
            StopCoroutine(m_shootCooldownCoroutine);

        m_shootCooldownCoroutine = StartCoroutine(ShootCooldownCoroutine());
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


    private IEnumerator ShootCooldownCoroutine()
    {
        yield return new WaitForSeconds(1f / m_shootSpeed);
        m_canWeaponFire = true;
    }

}
