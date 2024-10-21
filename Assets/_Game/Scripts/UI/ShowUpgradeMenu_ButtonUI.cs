using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowUpgradeMenu_ButtonUI : MonoBehaviour
{
    public static System.Action OnShowUpgradeUI_ButtonPressed;

    [SerializeField]
    private Button m_controlledButton = null;


    private void OnEnable()
    {
        m_controlledButton.onClick.AddListener(ShowUpgradeMenuUI_ButtonPressed);
    }

    private void OnDisable()
    {
        m_controlledButton.onClick.RemoveListener(ShowUpgradeMenuUI_ButtonPressed);
    }


    private void ShowUpgradeMenuUI_ButtonPressed()
    {
        OnShowUpgradeUI_ButtonPressed?.Invoke();
    }

}
