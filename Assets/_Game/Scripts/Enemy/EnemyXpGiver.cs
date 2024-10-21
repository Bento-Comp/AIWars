using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyXpGiver : MonoBehaviour
{
    [SerializeField]
    private XpGiver m_xpGiver = null;

    [SerializeField]
    private EnemyState m_enemyState = null;



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
        m_xpGiver.GiveXp();
    }
}
