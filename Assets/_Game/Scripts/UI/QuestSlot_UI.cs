using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestSlot_UI : MonoBehaviour
{
    public static System.Action<Quest> OnAskQuestInfo;

    [SerializeField]
    private RewardInfo_UI m_rewardInfoUI = null;

    [SerializeField]
    private GameObject m_questGoalSlotUIPrefab = null;

    [SerializeField]
    private Transform m_questGoalSlotUIParent = null;

    [SerializeField]
    private ClaimQuestReward_ButtonUI m_claimQuestReward_ButtonUI = null;

    private Quest m_currentQuest;


    private void OnEnable()
    {
        Quest.OnQuestInfoUpdate += OnQuestInfoUpdate;

        if (m_currentQuest != null)
            OnAskQuestInfo?.Invoke(m_currentQuest);
    }

    private void OnDisable()
    {
        Quest.OnQuestInfoUpdate -= OnQuestInfoUpdate;
    }


    public void Initialize(Quest quest)
    {
        m_currentQuest = quest;

        m_claimQuestReward_ButtonUI.Initialize(quest);

        if (m_currentQuest == null)
        {
            Debug.Log("Quest is null", gameObject);
            return;
        }

        for (int i = 0; i < m_currentQuest.QuestGoalList.Count; i++)
        {
            GameObject instantiatedQuestGoalLostUI = Instantiate(m_questGoalSlotUIPrefab, m_questGoalSlotUIParent);

            QuestGoalSlot_UI questGoalSlotUI = instantiatedQuestGoalLostUI.GetComponent<QuestGoalSlot_UI>();

            if (questGoalSlotUI == null)
            {
                Debug.LogError("Could not get QuestGoalSlot_UI component", gameObject);
                return;
            }

            questGoalSlotUI.Initialize(m_currentQuest.QuestGoalList[i]);
        }

    }

    private void OnQuestInfoUpdate(Quest quest)
    {
        if (quest == m_currentQuest)
            UpdateQuestInfo();
    }

    private void UpdateQuestInfo()
    {
        if (m_currentQuest == null)
        {
            Debug.Log("Quest is null", gameObject);
            return;
        }

        m_rewardInfoUI.UpdateRewardInfo(m_currentQuest.QuestReward);
        m_rewardInfoUI.UpdateRewardState(m_currentQuest.QuestReward.m_isRewardGranted);
    }

}
