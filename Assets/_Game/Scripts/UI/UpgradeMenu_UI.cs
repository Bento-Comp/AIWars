using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu_UI : Screen_UI
{
    [SerializeField]
    private Animator m_uiAnimator = null;


    private void OnEnable()
    {
        ShowUpgradeMenu_ButtonUI.OnShowUpgradeUI_ButtonPressed += ShowUpgradeUI;
        CloseUpgradeMenu_ButtonUI.OnCloseUpgradeUI_ButtonPressed += CloseUpgradeUI;

        ShowQuestMenu_ButtonUI.OnShowQuestUI_ButtonPressed += CloseUpgradeUI;
    }

    private void OnDisable()
    {
        ShowUpgradeMenu_ButtonUI.OnShowUpgradeUI_ButtonPressed -= ShowUpgradeUI;
        CloseUpgradeMenu_ButtonUI.OnCloseUpgradeUI_ButtonPressed -= CloseUpgradeUI;

        ShowQuestMenu_ButtonUI.OnShowQuestUI_ButtonPressed -= CloseUpgradeUI;
    }


    private void ShowUpgradeUI()
    {
        if (m_isUIOpen == false)
        {
            m_uiAnimator.SetTrigger("OpenUI");
            SetUIState(true);
        }
    }

    private void CloseUpgradeUI()
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
        CloseUpgradeUI();
    }

}
