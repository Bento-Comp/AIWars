using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseUpgradeMenu_ButtonUI : MonoBehaviour
{
    public static System.Action OnCloseUpgradeUI_ButtonPressed;

    [SerializeField]
    private Button m_controlledButton = null;


    private void OnEnable()
    {
        m_controlledButton.onClick.AddListener(CloseUpgradeMenuUI_ButtonPressed);
    }

    private void OnDisable()
    {
        m_controlledButton.onClick.RemoveListener(CloseUpgradeMenuUI_ButtonPressed);
    }


    private void CloseUpgradeMenuUI_ButtonPressed()
    {
        OnCloseUpgradeUI_ButtonPressed?.Invoke();
    }

}
