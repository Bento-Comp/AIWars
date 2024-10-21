using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Level_UI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text m_levelText = null;

    [SerializeField]
    private TMP_Text m_xpProgressionText = null;

    [SerializeField]
    private Slider m_xpProgressionSlider = null;



    private void OnEnable()
    {
        PlayerXP.OnBroadcastLevelAndXPInfo += OnBroadcastLevelAndXPInfo;
    }

    private void OnDisable()
    {
        PlayerXP.OnBroadcastLevelAndXPInfo -= OnBroadcastLevelAndXPInfo;
    }


    private void OnBroadcastLevelAndXPInfo(int level, float currentXP, float xpNeededToLevelUp)
    {
        m_levelText.text = level.ToString();

        if (xpNeededToLevelUp == 0)
        {
            m_xpProgressionText.text = "MAX";
            m_xpProgressionSlider.value = 1;
        }
        else
        {
            m_xpProgressionText.text = currentXP.ToString("F0") + "/" + xpNeededToLevelUp.ToString("F0");
            m_xpProgressionSlider.value = currentXP / xpNeededToLevelUp;
        }


    }

}
