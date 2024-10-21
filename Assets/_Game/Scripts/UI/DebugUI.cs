using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[System.Serializable]
public class DebugUIStat
{
    [SerializeField]
    private string m_statName = "Default";

    [SerializeField]
    private TMP_Text m_statText = null;

    [SerializeField]
    private PlayerStat m_playerStat = null;


    public void Enable()
    {
        m_playerStat.OnStatChange += OnStatChange;
    }

    public void Disable()
    {
        m_playerStat.OnStatChange -= OnStatChange;
    }

    private void OnStatChange()
    {
        m_statText.text = m_statName + " lvl: " + m_playerStat.StatCurrentLevel + " (" + "value: " + m_playerStat.GetStatValue() + ")";
    }

}



public class DebugUI : MonoBehaviour
{
    [SerializeField]
    private List<DebugUIStat> m_statUIList = null;

    [SerializeField]
    private TMP_Text m_gearText = null;

    [SerializeField]
    private TMP_Text m_levelText = null;

    private void OnEnable()
    {
        PlayerGearBag.OnBroadcastGearPosessed += OnBroadcastGearPosessed;
        PlayerXP.OnBroadcastLevel += OnBroadcastLevel;

        for (int i = 0; i < m_statUIList.Count; i++)
        {
            m_statUIList[i].Enable();
        }
    }

    private void OnDisable()
    {
        PlayerGearBag.OnBroadcastGearPosessed -= OnBroadcastGearPosessed;
        PlayerXP.OnBroadcastLevel -= OnBroadcastLevel;

        for (int i = 0; i < m_statUIList.Count; i++)
        {
            m_statUIList[i].Disable();
        }
    }

    private void OnBroadcastLevel(int playerLevel)
    {
        m_levelText.text = "Level : " + playerLevel.ToString();
    }

    private void OnBroadcastGearPosessed(float gearPosessed, float bagSize)
    {
        m_gearText.text = "Gear : " + gearPosessed.ToString("F0");
    }


}
