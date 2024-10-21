using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_KillEnemies : QuestGoal
{

    [SerializeField]
    private EnemyTypeEnum m_enemyTypeToTrack = EnemyTypeEnum.Small;


    protected override void OnEnable()
    {
        base.OnEnable();
        EnemyType.OnEnemyTypeKilled += OnEnemyTypeKilled;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        EnemyType.OnEnemyTypeKilled -= OnEnemyTypeKilled;
    }


    private void OnEnemyTypeKilled(EnemyTypeEnum enemyType)
    {
        if (enemyType != m_enemyTypeToTrack)
            return;

        IncreaseProgression(1);
    }


}
