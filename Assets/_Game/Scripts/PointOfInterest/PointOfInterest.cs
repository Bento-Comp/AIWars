using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointOfInterest : MonoBehaviour
{
    public static System.Action<Quest> OnShowQuestInfo;
    public static System.Action<Quest> OnHideQuestInfo;

    public static System.Action<Quest> OnQuestAccepted;
    public static System.Action<Quest> OnQuestRefused;


    [SerializeField]
    private ColliderObjectDetector m_playerDetector = null;

    [SerializeField]
    private GameObject m_exclamation = null;

    [SerializeField]
    private Quest m_quest = null;

    private bool m_isPointOfInterestSelected;

    private void OnEnable()
    {
        EnableInteractionWithPlayer();

        Quest_Manager.OnGiveUpQuest += OnGiveUpQuest;
        QuestProposal_Accept_Button.OnAcceptQuest_ButtonPressed += OnAcceptQuest_ButtonPressed;
        QuestProposal_Refuse_Button.OnRefuseQuest_ButtonPressed += OnRefuseQuest_ButtonPressed;
        Quest.OnQuestAlreadyDone += OnQuestAlreadyDone;
        Quest.OnLoadActiveQuest += OnLoadActiveQuest;
    }

    private void OnDisable()
    {
        DisableInterationWithPlayer();

        Quest_Manager.OnGiveUpQuest -= OnGiveUpQuest;
        QuestProposal_Accept_Button.OnAcceptQuest_ButtonPressed -= OnAcceptQuest_ButtonPressed;
        QuestProposal_Refuse_Button.OnRefuseQuest_ButtonPressed -= OnRefuseQuest_ButtonPressed;
        Quest.OnQuestAlreadyDone -= OnQuestAlreadyDone;
        Quest.OnLoadActiveQuest -= OnLoadActiveQuest;
    }


    private void EnableInteractionWithPlayer()
    {
        m_playerDetector.OnObjectDetected += OnPlayerDetected;
        m_playerDetector.OnObjectNotDetectedAnymore += OnPlayerNotDetected;
        m_exclamation.SetActive(true);
    }

    private void DisableInterationWithPlayer()
    {
        m_playerDetector.OnObjectDetected -= OnPlayerDetected;
        m_playerDetector.OnObjectNotDetectedAnymore -= OnPlayerNotDetected;
        m_exclamation.SetActive(false);
    }

    private void OnLoadActiveQuest(Quest quest)
    {
        if (m_quest != quest)
            return;

        DisableInterationWithPlayer();
    }

    private void OnQuestAlreadyDone(Quest quest)
    {
        if (quest != m_quest)
            return;

        DisableInterationWithPlayer();
    }

    private void OnGiveUpQuest(Quest quest)
    {
        if (quest != m_quest)
            return;

        EnableInteractionWithPlayer();
    }

    private void OnAcceptQuest_ButtonPressed()
    {
        if (m_isPointOfInterestSelected == false)
            return;

        OnQuestAccepted?.Invoke(m_quest);
        OnHideQuestInfo?.Invoke(m_quest);

        DisableInterationWithPlayer();

        m_isPointOfInterestSelected = false;
    }

    private void OnRefuseQuest_ButtonPressed()
    {
        OnQuestRefused?.Invoke(m_quest);
        OnHideQuestInfo?.Invoke(m_quest);
        m_isPointOfInterestSelected = false;
    }

    private void OnPlayerDetected(GameObject playerTriggerCollider)
    {
        m_isPointOfInterestSelected = true;
        OnShowQuestInfo?.Invoke(m_quest);
    }

    private void OnPlayerNotDetected(GameObject playerTriggerCollider)
    {
        m_isPointOfInterestSelected = false;
        OnHideQuestInfo?.Invoke(m_quest);
    }

}
