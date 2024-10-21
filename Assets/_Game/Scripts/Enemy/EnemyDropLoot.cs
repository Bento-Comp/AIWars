using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropLoot : MonoBehaviour
{
    [SerializeField]
    private EnemyState m_enemyState = null;

    [SerializeField]
    private Rigidbody m_body = null;

    [SerializeField]
    private GearSpawner m_gearSpawner = null;



    private void OnEnable()
    {
        m_enemyState.OnDisableEnemy += OnDisableEnemy;
    }

    private void OnDisable()
    {
        m_enemyState.OnDisableEnemy -= OnDisableEnemy;
    }


    private void OnDisableEnemy()
    {
        m_gearSpawner.SpawnGear(m_body.position);
    }

}
