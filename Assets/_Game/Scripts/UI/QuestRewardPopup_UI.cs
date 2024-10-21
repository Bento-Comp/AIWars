using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestRewardPopup_UI : MonoBehaviour
{
    [SerializeField]
    private GameObject m_goldRewardIcon = null;

    [SerializeField]
    private GameObject m_xpRewardIcon = null;

    [SerializeField]
    private TMP_Text m_valueText = null;

    [SerializeField]
    private TMP_Text m_questGoalText = null;


    public void ShowRewardPopup(QuestReward questReward, Quest quest)
    {
        m_goldRewardIcon.SetActive(questReward.m_questRewardType == QuestRewardType.Gold);
        m_xpRewardIcon.SetActive(questReward.m_questRewardType == QuestRewardType.XP);

        m_valueText.text = questReward.m_amount.ToString("F0");

        m_questGoalText.text = quest.QuestName;
    }


    public void ShowRewardPopup(QuestReward questReward, QuestGoal questGoal)
    {
        m_goldRewardIcon.SetActive(questReward.m_questRewardType == QuestRewardType.Gold);
        m_xpRewardIcon.SetActive(questReward.m_questRewardType == QuestRewardType.XP);

        m_valueText.text = questReward.m_amount.ToString("F0");

        m_questGoalText.text = questGoal.QuestGoalName;
    }

}
