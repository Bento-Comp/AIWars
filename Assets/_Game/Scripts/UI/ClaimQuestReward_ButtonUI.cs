using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClaimQuestReward_ButtonUI : MonoBehaviour
{
    public static System.Action<Quest> OnClaimQuestReward_ButtonPressed;

    private Quest m_currentQuest;


    [SerializeField]
    private Button m_controlledButton = null;


    private void OnEnable()
    {
        m_controlledButton.onClick.AddListener(ClaimQuestReward_ButtonPressed);
        Quest.OnQuestAlreadyDone += OnQuestAlreadyDone;

        CheckQuestState();
    }

    private void CheckQuestState()
    {
        if (m_currentQuest == null)
            return;

        if (m_currentQuest.IsQuestCompleted == true && m_currentQuest.QuestReward.m_isRewardGranted == false)
            m_controlledButton.interactable = true;
        else
            m_controlledButton.interactable = false;
    }

    private void OnDisable()
    {
        m_controlledButton.onClick.RemoveListener(ClaimQuestReward_ButtonPressed);
        Quest.OnQuestAlreadyDone -= OnQuestAlreadyDone;
    }


    public void Initialize(Quest quest)
    {
        m_currentQuest = quest;
    }

    private void OnQuestAlreadyDone(Quest quest)
    {
        if (quest != m_currentQuest)
            return;

        m_controlledButton.interactable = false;
    }

    private void ClaimQuestReward_ButtonPressed()
    {
        OnClaimQuestReward_ButtonPressed?.Invoke(m_currentQuest);
        CheckQuestState();
    }

}
