using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyTypeEnum
{
    Small,
    Tanky,
    Attacker
}

public class EnemyType : MonoBehaviour
{
    public static System.Action<EnemyTypeEnum> OnEnemyTypeKilled;

    [SerializeField]
    private EnemyState m_enemyState = null;

    [SerializeField]
    private EnemyTypeEnum m_enemyType = EnemyTypeEnum.Small;



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
        OnEnemyTypeKilled?.Invoke(m_enemyType);
    }

}
