using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGoalRewardsPopup_UI : MonoBehaviour
{
    [SerializeField]
    private GameObject m_questRewardPopupUIPrefab = null;

    [SerializeField]
    private Transform m_questRewardPopupUIParent = null;


    private void OnEnable()
    {
        QuestReward.OnGiveQuestReward += OnGiveQuestReward;
        QuestReward.OnGiveQuestGoalReward += OnGiveQuestGoalReward;
    }

    private void OnDisable()
    {
        QuestReward.OnGiveQuestReward -= OnGiveQuestReward;
        QuestReward.OnGiveQuestGoalReward -= OnGiveQuestGoalReward;
    }


    private void OnGiveQuestReward(QuestReward questReward, Quest quest)
    {
        GameObject instantiatedQuestRewardPopupUI = Instantiate(m_questRewardPopupUIPrefab, m_questRewardPopupUIParent);


        QuestRewardPopup_UI questRewardPopupUI = instantiatedQuestRewardPopupUI.GetComponent<QuestRewardPopup_UI>();

        if (questRewardPopupUI == null)
        {
            Debug.LogError("Could not retreive QuestRewardPopup_UI component", gameObject);
            return;
        }

        questRewardPopupUI.ShowRewardPopup(questReward, quest);

    }


    private void OnGiveQuestGoalReward(QuestReward questReward, QuestGoal questGoal)
    {
        GameObject instantiatedQuestRewardPopupUI = Instantiate(m_questRewardPopupUIPrefab, m_questRewardPopupUIParent);


        QuestRewardPopup_UI questRewardPopupUI = instantiatedQuestRewardPopupUI.GetComponent<QuestRewardPopup_UI>();

        if (questRewardPopupUI == null)
        {
            Debug.LogError("Could not retreive QuestRewardPopup_UI component", gameObject);
            return;
        }

        questRewardPopupUI.ShowRewardPopup(questReward, questGoal);

    }

}
