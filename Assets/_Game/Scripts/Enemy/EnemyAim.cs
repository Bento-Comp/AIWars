using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAim : MonoBehaviour
{
    public System.Action OnLockAimDirection;

    [SerializeField]
    private EnemyState m_enemyState = null;

    [SerializeField]
    private EnemyDetector m_enemyDetector = null;

    [SerializeField]
    private EnemyShoot m_enemyShoot = null;

    [SerializeField]
    private Transform m_weaponPivotTransform = null;

    [SerializeField]
    private float m_rotationSpeed = 45f;

    [SerializeField]
    private float m_timeBeforeLockingAimDirection = 1.5f;


    private float m_timer;
    private bool m_canAim;
    private bool m_isEnabled;


    private void OnEnable()
    {
        m_enemyShoot.OnShootCooldownOver += OnShootCooldownOver;

        m_enemyState.OnInitialize += OnInitialize;
        m_enemyState.OnDisableEnemy += OnDisableEnemy;
    }

    private void OnDisable()
    {
        m_enemyShoot.OnShootCooldownOver -= OnShootCooldownOver;

        m_enemyState.OnInitialize += OnInitialize;
        m_enemyState.OnDisableEnemy += OnDisableEnemy;
    }

    private void Update()
    {
        Aim();
    }


    private void OnInitialize()
    {
        m_canAim = true;
        m_isEnabled = true;
    }

    private void OnDisableEnemy()
    {
        m_isEnabled = false;
    }

    private void OnShootCooldownOver()
    {
        m_canAim = true;
    }

    private void Aim()
    {
        if (m_isEnabled == false)
            return;

        if (m_enemyDetector.HasTarget && m_canAim)
        {
            m_timer += Time.deltaTime;

            RotateWeaponPivot();

            if(m_timer > m_timeBeforeLockingAimDirection)
            {
                m_canAim = false;
                OnLockAimDirection?.Invoke();
            }
        }
        else
        {
            m_timer = 0;
        }
    }

    private void RotateWeaponPivot()
    {
        Vector3 targetDirection = m_enemyDetector.NearestObject.transform.position - m_weaponPivotTransform.position;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        m_weaponPivotTransform.rotation = Quaternion.Slerp(m_weaponPivotTransform.rotation, targetRotation, m_rotationSpeed * Time.deltaTime);
    }
}
