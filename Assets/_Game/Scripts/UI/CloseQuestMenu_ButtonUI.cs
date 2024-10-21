using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseQuestMenu_ButtonUI : MonoBehaviour
{
    public static System.Action OnCloseQuestUI_ButtonPressed;

    [SerializeField]
    private Button m_controlledButton = null;


    private void OnEnable()
    {
        m_controlledButton.onClick.AddListener(CloseQuestMenuUI_ButtonPressed);
    }

    private void OnDisable()
    {
        m_controlledButton.onClick.RemoveListener(CloseQuestMenuUI_ButtonPressed);
    }


    private void CloseQuestMenuUI_ButtonPressed()
    {
        OnCloseQuestUI_ButtonPressed?.Invoke();
    }
}
