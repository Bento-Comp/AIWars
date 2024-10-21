using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowQuestMenu_ButtonUI : MonoBehaviour
{
    public static System.Action OnShowQuestUI_ButtonPressed;

    [SerializeField]
    private Button m_controlledButton = null;


    private void OnEnable()
    {
        m_controlledButton.onClick.AddListener(ShowQuestMenuUI_ButtonPressed);
    }

    private void OnDisable()
    {
        m_controlledButton.onClick.RemoveListener(ShowQuestMenuUI_ButtonPressed);
    }


    private void ShowQuestMenuUI_ButtonPressed()
    {
        OnShowQuestUI_ButtonPressed?.Invoke();
    }
}
