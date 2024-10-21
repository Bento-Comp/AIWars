using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Upgrades : QuestGoal
{

    [SerializeField]
    private StatType m_statTypeToTrack;


    protected override void OnEnable()
    {
        base.OnEnable();
        PlayerStat.OnUpgradePurchased += OnUpgradePurchased;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        PlayerStat.OnUpgradePurchased -= OnUpgradePurchased;
    }


    private void OnUpgradePurchased(StatType statType, float upgradeCost)
    {
        if (m_statTypeToTrack != statType)
            return;

        IncreaseProgression(1);
    }


}
