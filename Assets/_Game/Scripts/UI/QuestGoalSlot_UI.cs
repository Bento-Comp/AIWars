using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestGoalSlot_UI : MonoBehaviour
{
    public static System.Action<QuestGoal> OnAskQuestGoalInfo;

    [SerializeField]
    private RewardInfo_UI m_rewardInfoUI = null;

    [SerializeField]
    private TMP_Text m_questGoalName = null;

    [SerializeField]
    private TMP_Text m_questGoalProgression = null;

    [SerializeField]
    private Slider m_progressionSlider = null;

    private QuestGoal m_currentQuestGoal;


    private void OnEnable()
    {
        QuestGoal.OnQuestGoalInfoUpdate += OnQuestGoalUpdate;

        if (m_currentQuestGoal != null)
            OnAskQuestGoalInfo?.Invoke(m_currentQuestGoal);
    }

    private void OnDisable()
    {
        QuestGoal.OnQuestGoalInfoUpdate -= OnQuestGoalUpdate;
    }



    public void Initialize(QuestGoal questGoal)
    {
        m_currentQuestGoal = questGoal;

        if (m_currentQuestGoal == null)
        {
            Debug.Log("QuestGoal is null", gameObject);
            return;
        }
    }


    private void OnQuestGoalUpdate(QuestGoal questGoal)
    {
        if (questGoal == m_currentQuestGoal)
            UpdateQuestGoalInfo();
    }

    private void UpdateQuestGoalInfo()
    {
        if (m_currentQuestGoal == null)
        {
            Debug.Log("QuestGoal is null", gameObject);
            return;
        }

        m_questGoalName.text = m_currentQuestGoal.QuestGoalName;
        m_questGoalProgression.text = m_currentQuestGoal.TargetCountProgression.ToString() + " / " + m_currentQuestGoal.TargetCount.ToString();

        m_progressionSlider.value = (float)m_currentQuestGoal.TargetCountProgression / m_currentQuestGoal.TargetCount;

        m_rewardInfoUI.UpdateRewardState(m_currentQuestGoal.QuestGoalReward.m_isRewardGranted);
        m_rewardInfoUI.UpdateRewardInfo(m_currentQuestGoal.QuestGoalReward);
    }




}
