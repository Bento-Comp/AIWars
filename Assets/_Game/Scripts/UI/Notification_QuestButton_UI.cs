using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notification_QuestButton_UI : MonoBehaviour
{
    [SerializeField]
    private GameObject m_notificationIcon = null;


    private void Awake()
    {
        m_notificationIcon.SetActive(false);
    }

    private void OnEnable()
    {
        Quest.OnQuestInfoUpdate += OnQuestInfoUpdate;
        Quest.OnQuestCompleted += OnQuestCompleted;
    }

    private void OnDisable()
    {
        Quest.OnQuestInfoUpdate -= OnQuestInfoUpdate;
        Quest.OnQuestCompleted -= OnQuestCompleted;
    }


    private void OnQuestInfoUpdate(Quest quest)
    {
        m_notificationIcon.SetActive(quest.IsQuestCompleted);
    }

    private void OnQuestCompleted(Quest quest)
    {
        m_notificationIcon.SetActive(false);
    }
}
