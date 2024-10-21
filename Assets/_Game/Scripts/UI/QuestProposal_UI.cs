using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestProposal_UI : Screen_UI
{
    [SerializeField]
    private Animator m_uiAnimator = null;

    [SerializeField]
    private TMP_Text m_questDescription = null;


    private void OnEnable()
    {
        PointOfInterest.OnShowQuestInfo += OnShowQuestInfo;
        PointOfInterest.OnHideQuestInfo += OnHideQuestInfo;


    }

    private void OnDisable()
    {
        PointOfInterest.OnShowQuestInfo -= OnShowQuestInfo;
        PointOfInterest.OnHideQuestInfo -= OnHideQuestInfo;
    }



    private void OnShowQuestInfo(Quest quest)
    {
        if (m_isUIOpen == false)
        {
            m_uiAnimator.SetTrigger("OpenUI");
            SetUIState(true);
            m_questDescription.text = quest.QuestName;
        }
    }

    private void OnHideQuestInfo(Quest quest)
    {
        if (m_isUIOpen == true)
        {
            m_uiAnimator.SetTrigger("CloseUI");
            SetUIState(false);
        }
    }

}
