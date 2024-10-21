using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RewardInfo_UI : MonoBehaviour
{
    [SerializeField]
    private GameObject m_goldRewardIcon = null;

    [SerializeField]
    private GameObject m_xpRewardIcon = null;

    [SerializeField]
    private GameObject m_taskCompleteIcon = null;

    [SerializeField]
    private TMP_Text m_rewardValueText = null;


    public void UpdateRewardInfo(QuestReward questReward)
    {
        m_goldRewardIcon.SetActive(questReward.m_questRewardType == QuestRewardType.Gold);
        m_xpRewardIcon.SetActive(questReward.m_questRewardType == QuestRewardType.XP);

        m_rewardValueText.text = questReward.m_amount.ToString("F0");
    }


    public void UpdateRewardState(bool isTaskComplete)
    {
        m_taskCompleteIcon.SetActive(isTaskComplete);
    }
}
