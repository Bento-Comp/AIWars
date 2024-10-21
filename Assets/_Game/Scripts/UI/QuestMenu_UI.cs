using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMenu_UI : Screen_UI
{
    [SerializeField]
    private Animator m_uiAnimator = null;

    [SerializeField]
    private GameObject m_questSlotUIPrefab = null;

    [SerializeField]
    private Transform m_questSlotUIParent = null;


    private void OnEnable()
    {
        ShowQuestMenu_ButtonUI.OnShowQuestUI_ButtonPressed += ShowQuestUI;
        CloseQuestMenu_ButtonUI.OnCloseQuestUI_ButtonPressed += CloseQuestUI;

        PointOfInterest.OnQuestAccepted += OnQuestAccepted;

        ShowUpgradeMenu_ButtonUI.OnShowUpgradeUI_ButtonPressed += CloseQuestUI;

        Quest.OnQuestInitialize += OnQuestInitialize;
        Quest.OnQuestCompleted += OnQuestCompleted;
    }

    private void OnDisable()
    {
        ShowQuestMenu_ButtonUI.OnShowQuestUI_ButtonPressed -= ShowQuestUI;
        CloseQuestMenu_ButtonUI.OnCloseQuestUI_ButtonPressed -= CloseQuestUI;

        PointOfInterest.OnQuestAccepted -= OnQuestAccepted;

        ShowUpgradeMenu_ButtonUI.OnShowUpgradeUI_ButtonPressed -= CloseQuestUI;

        Quest.OnQuestInitialize -= OnQuestInitialize;
        Quest.OnQuestCompleted -= OnQuestCompleted;
    }


    private void ShowQuestUI()
    {
        if (m_isUIOpen == false)
        {
            m_uiAnimator.SetTrigger("OpenUI");
            SetUIState(true);
        }
    }

    private void CloseQuestUI()
    {
        if (m_isUIOpen == true)
        {
            m_uiAnimator.SetTrigger("CloseUI");
            SetUIState(false);
        }
    }


    //called by button
    public void CloseUI()
    {
        CloseQuestUI();
    }

    private void OnQuestAccepted(Quest quest)
    {
        ShowQuestUI();
    }

    private void OnQuestCompleted(Quest quest)
    {
        StartCoroutine(CompleteQuestCoroutine());
    }

    private IEnumerator CompleteQuestCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        RemoveQuestSlot();
        yield return new WaitForEndOfFrame();
        CloseQuestUI();
    }

    private void RemoveQuestSlot()
    {
        DestroyTransformChildren.DestroyAllTransformChildren(m_questSlotUIParent);
    }

    private void OnQuestInitialize(Quest quest)
    {
        GameObject instantiatedQuestSlotUI = Instantiate(m_questSlotUIPrefab, m_questSlotUIParent);

        QuestSlot_UI questSlotUI = instantiatedQuestSlotUI.GetComponent<QuestSlot_UI>();

        if(questSlotUI == null)
        {
            Debug.LogError("Could not get QuestSlot_UI component", gameObject);
            return;
        }

        questSlotUI.Initialize(quest);
    }



}
