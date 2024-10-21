using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerXpChart", order = 1)]
public class PlayerXpChart_ScriptableObject : ScriptableObject
{
    [SerializeField]
    private List<float> m_xpList = null;

    public int GetMaxLevel()
    {
        return m_xpList.Count + 1;
    }

    public bool HasLevelUp(int currentLevel, float currentXp)
    {
        if (currentLevel < 1)
        {
            Debug.LogError("Current level parameter is wrong");
            return false;
        }

        int index = currentLevel - 1;

        if (index > m_xpList.Count - 1)
            return false;

        if (currentXp >= m_xpList[index])
            return true;

        return false;
    }


    public float GetRequiredXPToNextLevel(int currentLevel)
    {
        int index = currentLevel - 1;

        if(index >= m_xpList.Count)
            return m_xpList[m_xpList.Count - 1];

        return m_xpList[index];

    }

    // If the player is level 2, this method will return the value of xp needed to get to level 2 (i.e. the total xp needed to get to level 2)
    public float GetXPFromCurrentLevel(int currentLevel)
    {
        if (currentLevel <= 1)
            return 0;

        int index = currentLevel - 2;

        if (index > m_xpList.Count)
            return m_xpList[m_xpList.Count - 1];

        return m_xpList[index];
    }

}
