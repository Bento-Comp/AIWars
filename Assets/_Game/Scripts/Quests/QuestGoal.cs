using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class QuestGoal : MonoBehaviour
{
    public System.Action OnQuestGoalReached;
    public static System.Action<QuestGoal> OnQuestGoalInfoUpdate;

    [SerializeField]
    private string m_questGoalName = "";

    [SerializeField]
    private string m_questGoalID = "";

    [SerializeField]
    private QuestReward m_questGoalReward = null;

    [SerializeField, Range(0, 100000)]
    private int m_targetCount = 0;


    private int m_targetCountProgression;
    private bool m_isQuestGoalReached;


    public QuestReward QuestGoalReward { get => m_questGoalReward; }
    public string QuestGoalName { get => m_questGoalName; }
    public int TargetCountProgression { get => m_targetCountProgression; }
    public int TargetCount { get => m_targetCount; }
    public bool IsQuestGoalReached { get => m_isQuestGoalReached; }


    protected virtual void OnEnable()
    {
        QuestGoalSlot_UI.OnAskQuestGoalInfo += SendQuestGoalInfo;
    }

    protected virtual void OnDisable()
    {
        QuestGoalSlot_UI.OnAskQuestGoalInfo -= SendQuestGoalInfo;
    }

    private void SendQuestGoalInfo(QuestGoal questGoal)
    {
        if (questGoal == this)
            OnQuestGoalInfoUpdate?.Invoke(this);
    }


    //Called by Quest script
    public void InitalizeQuestGoal()
    {
        LoadQuestGoalState();
    }

    private void SaveQuestGoalState()
    {
        PlayerPrefs.SetInt(m_questGoalID + "state", m_isQuestGoalReached == false ? 0 : 1);
        PlayerPrefs.SetInt(m_questGoalID + "progression", m_targetCountProgression);
    }

    private void LoadQuestGoalState()
    {
        if (PlayerPrefs.HasKey(m_questGoalID + "state") == false || PlayerPrefs.HasKey(m_questGoalID + "progression") == false)
            SaveQuestGoalState();
        else
        {
            m_isQuestGoalReached = PlayerPrefs.GetInt(m_questGoalID + "state") == 0 ? false : true;
            m_targetCountProgression = PlayerPrefs.GetInt(m_questGoalID + "progression");
        }

        m_questGoalReward.m_isRewardGranted = m_isQuestGoalReached;

        OnQuestGoalInfoUpdate?.Invoke(this);
    }


    public void ResetQuestGoal()
    {
        m_isQuestGoalReached = false;
        m_targetCountProgression = 0;
        SaveQuestGoalState();
    }

    protected void IncreaseProgression(int amount)
    {
        if (m_isQuestGoalReached == true)
            return;

        m_targetCountProgression += amount;

        CheckQuestGoalCompletionState();

        SaveQuestGoalState();

        OnQuestGoalInfoUpdate?.Invoke(this);
    }

    private void CheckQuestGoalCompletionState()
    {
        if (m_targetCountProgression >= m_targetCount)
        {
            m_isQuestGoalReached = true;
            OnQuestGoalReached?.Invoke();
            GiveReward();
        }
    }

    private void GiveReward()
    {
        m_questGoalReward.GiveReward(this);

        OnQuestGoalInfoUpdate?.Invoke(this);
    }

}
