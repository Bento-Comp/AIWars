using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Manager : MonoBehaviour
{
    public static System.Action<Quest> OnGiveUpQuest;


    private Quest m_activeQuest;


    private void OnEnable()
    {
        PointOfInterest.OnQuestAccepted += OnQuestAccepted;
        Quest.OnQuestCompleted += OnQuestCompleted;
        Quest.OnLoadActiveQuest += OnLoadActiveQuest;
    }

    private void OnDisable()
    {
        PointOfInterest.OnQuestAccepted -= OnQuestAccepted;
        Quest.OnQuestCompleted -= OnQuestCompleted;
        Quest.OnLoadActiveQuest -= OnLoadActiveQuest;
    }

    private void OnLoadActiveQuest(Quest quest)
    {
        m_activeQuest = quest;
    }

    private void OnQuestAccepted(Quest acceptedQuest)
    {
        if (m_activeQuest != null)
        {
            OnGiveUpQuest?.Invoke(m_activeQuest);
            m_activeQuest.ToggleQuest(false);
        }

        m_activeQuest = acceptedQuest;
        m_activeQuest.ToggleQuest(true);
    }


    private void OnQuestCompleted(Quest completedQuest)
    {
        if (completedQuest != m_activeQuest)
            return;

        m_activeQuest.ToggleQuest(false);

        m_activeQuest = null;
    }

}
