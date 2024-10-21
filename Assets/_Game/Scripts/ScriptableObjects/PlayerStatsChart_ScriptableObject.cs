using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerStatChart", order = 1)]
public class PlayerStatsChart_ScriptableObject : ScriptableObject
{
    [SerializeField]
    private AnimationCurve m_statValueCurve = null;

    [SerializeField]
    private AnimationCurve m_statCostCurve = null;



    public float GetStatValue(int playerLevel)
    {
        return m_statValueCurve.Evaluate(playerLevel);
    }

    public float GetStatCost(int playerLevel)
    {
        return m_statCostCurve.Evaluate(playerLevel);
    }

    public float GetStatValueNextLevel(int playerLevel)
    {
        return m_statValueCurve.Evaluate(playerLevel + 1);
    }
}
