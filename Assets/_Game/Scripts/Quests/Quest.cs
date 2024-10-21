using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(1)] //allows all the subsciption in the other script before this script is initialized
public class Quest : MonoBehaviour
{
    public static System.Action<Quest> OnQuestCompleted;
    public static System.Action<Quest> OnQuestInitialize;
    public static System.Action<Quest> OnQuestInfoUpdate;
    public static System.Action<Quest> OnQuestAlreadyDone;
    public static System.Action<QuestGoal> OnResetQuestGoal;
    public static System.Action<Quest> OnLoadActiveQuest;

    [SerializeField]
    private string m_questID = "";

    [SerializeField]
    private string m_questName = "";

    [SerializeField]
    private QuestReward m_questReward = null;

    [SerializeField]
    private List<QuestGoal> m_questGoalList = null;


    private bool m_isQuestCompleted;
    private bool m_isQuestActive;

    public List<QuestGoal> QuestGoalList { get => m_questGoalList; }
    public QuestReward QuestReward { get => m_questReward; }
    public string QuestName { get => m_questName; }
    public bool IsQuestCompleted { get => m_isQuestCompleted; }

    private void Awake()
    {
        LoadQuestState();
    }

    private void OnEnable()
    {
        for (int i = 0; i < m_questGoalList.Count; i++)
        {
            m_questGoalList[i].OnQuestGoalReached += OnQuestGoalReached;
        }

        QuestSlot_UI.OnAskQuestInfo += SendQuestInfo;
        ClaimQuestReward_ButtonUI.OnClaimQuestReward_ButtonPressed += OnClaimQuestReward_ButtonPressed;
        Quest_Manager.OnGiveUpQuest += OnGiveUpQuest;


        LoadQuestState();
        InitializeQuest();

    }

    private void OnDisable()
    {
        for (int i = 0; i < m_questGoalList.Count; i++)
        {
            m_questGoalList[i].OnQuestGoalReached -= OnQuestGoalReached;
        }

        QuestSlot_UI.OnAskQuestInfo -= SendQuestInfo;
        ClaimQuestReward_ButtonUI.OnClaimQuestReward_ButtonPressed -= OnClaimQuestReward_ButtonPressed;
        Quest_Manager.OnGiveUpQuest -= OnGiveUpQuest;
    }



    private void LoadQuestState()
    {
        if (PlayerPrefs.HasKey(m_questID + "isCompleted") == false || PlayerPrefs.HasKey(m_questID + "isActive") == false)
        {
            m_isQuestActive = false;
            m_isQuestCompleted = false;
            SaveQuestState();
        }
        else
        {
            m_isQuestCompleted = PlayerPrefs.GetInt(m_questID + "isCompleted") == 0 ? false : true;
            m_isQuestActive = PlayerPrefs.GetInt(m_questID + "isActive") == 0 ? false : true;
        }

    }

    private void SaveQuestState()
    {
        PlayerPrefs.SetInt(m_questID + "isCompleted", m_isQuestCompleted == false ? 0 : 1);
        PlayerPrefs.SetInt(m_questID + "isActive", m_isQuestActive == false ? 0 : 1);
    }

    private void InitializeQuest()
    {
        if (m_isQuestCompleted)
        {
            OnQuestAlreadyDone?.Invoke(this);
        }

        if (m_isQuestActive)
        {
            OnLoadActiveQuest?.Invoke(this);

            OnQuestInitialize?.Invoke(this);

            for (int i = 0; i < m_questGoalList.Count; i++)
            {
                m_questGoalList[i].InitalizeQuestGoal();
            }

            OnQuestInfoUpdate?.Invoke(this);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void ToggleQuest(bool state)
    {
        m_isQuestActive = state;
        SaveQuestState();
        gameObject.SetActive(state);
    }

    private void OnGiveUpQuest(Quest quest)
    {
        if (quest != this)
            return;

        ResetQuest();

        m_isQuestActive = false;

    }

    private void ResetQuest()
    {
        for (int i = 0; i < m_questGoalList.Count; i++)
        {
            m_questGoalList[i].ResetQuestGoal();
        }

        SaveQuestState();
    }

    private void SendQuestInfo(Quest quest)
    {
        if (quest == this)
            OnQuestInfoUpdate?.Invoke(this);
    }

    private void OnQuestGoalReached()
    {
        if (m_isQuestCompleted)
            return;

        m_isQuestCompleted = CheckCompeltionState();

        OnQuestInfoUpdate?.Invoke(this);

        SaveQuestState();
    }

    private void OnClaimQuestReward_ButtonPressed(Quest quest)
    {
        if (quest != this)
            return;

        if (m_isQuestCompleted == false)
            return;

        GiveReward();

        OnQuestCompleted?.Invoke(this);

        SaveQuestState();
    }

    private bool CheckCompeltionState()
    {
        for (int i = 0; i < m_questGoalList.Count; i++)
        {
            if (m_questGoalList[i].IsQuestGoalReached == false)
                return false;
        }

        return true;
    }


    private void GiveReward()
    {
        m_questReward.GiveReward(this);

        OnQuestInfoUpdate?.Invoke(this);
    }

}
