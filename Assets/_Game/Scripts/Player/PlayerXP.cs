using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerXP : MonoBehaviour
{
    public static System.Action<int> OnLevelUp;
    public static System.Action<int> OnBroadcastLevel;
    public static System.Action<int> OnBroadcastMaxLevel;
    //int : level; int : current xp; int : required xp
    public static System.Action<int, float, float> OnBroadcastLevelAndXPInfo;


    [SerializeField]
    private PlayerXpChart_ScriptableObject m_xpChart = null;

    private string m_playerXPKey = "playerXP";
    private string m_playerLevelKey = "playerLevel";

    private float m_xpTotal;
    private int m_currentLevel;


    private void OnEnable()
    {
        XpGiver.OnGiveXp += GainXP;

        LevelDesignTools.OnLevelUp += LevelDesignTools_OnLevelUp;

        QuestReward.OnGiveReward += OnGiveReward;
    }

    private void OnDisable()
    {
        XpGiver.OnGiveXp -= GainXP;

        LevelDesignTools.OnLevelUp -= LevelDesignTools_OnLevelUp;

        QuestReward.OnGiveReward -= OnGiveReward;
    }


    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        LoadLevelAndXp();
    }

    private void LoadLevelAndXp()
    {
        OnBroadcastMaxLevel?.Invoke(m_xpChart.GetMaxLevel());

        if (PlayerPrefs.HasKey(m_playerLevelKey) == false)
        {
            m_currentLevel = 1;
            PlayerPrefs.SetInt(m_playerLevelKey, m_currentLevel);
        }
        else
        {
            m_currentLevel = PlayerPrefs.GetInt(m_playerLevelKey);
        }

        OnBroadcastLevel?.Invoke(m_currentLevel);



        if (PlayerPrefs.HasKey(m_playerXPKey) == false)
        {
            m_xpTotal = 0;
            PlayerPrefs.SetFloat(m_playerXPKey, m_xpTotal);
        }
        else
        {
            m_xpTotal = PlayerPrefs.GetFloat(m_playerXPKey);
        }


        SendXPAndLevelInfos();
    }

    private void SaveLevelAndXP()
    {
        PlayerPrefs.SetInt(m_playerLevelKey, m_currentLevel);
        PlayerPrefs.SetFloat(m_playerXPKey, m_xpTotal);
    }

    private void SendXPAndLevelInfos()
    {
        OnBroadcastLevelAndXPInfo?.Invoke(m_currentLevel,
                    m_xpTotal - m_xpChart.GetXPFromCurrentLevel(m_currentLevel),
                    m_xpChart.GetRequiredXPToNextLevel(m_currentLevel) - m_xpChart.GetXPFromCurrentLevel(m_currentLevel));
    }


    private void LevelDesignTools_OnLevelUp()
    {
        GainXP(m_xpChart.GetRequiredXPToNextLevel(m_currentLevel) - (m_xpTotal - m_xpChart.GetXPFromCurrentLevel(m_currentLevel)));
    }


    private void OnGiveReward(QuestReward questReward)
    {
        if (questReward.m_questRewardType == QuestRewardType.XP)
        {
            GainXP(questReward.m_amount);
        }
    }

    private void GainXP(float gainedXp)
    {
        m_xpTotal += gainedXp;

        if (m_xpChart.HasLevelUp(m_currentLevel, m_xpTotal))
        {
            m_currentLevel++;
            OnLevelUp?.Invoke(m_currentLevel);
            OnBroadcastLevel?.Invoke(m_currentLevel);
        }

        SaveLevelAndXP();
        SendXPAndLevelInfos();
    }




}
