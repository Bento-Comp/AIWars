using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen_UI : MonoBehaviour
{
    public static System.Action OnAnyScreenUIOpen;
    public static System.Action OnNoScreenUIOpen;

    private static int m_uiOpenCount;

    protected bool m_isUIOpen;

    protected bool IsAnyUIOpen { get => m_uiOpenCount > 0; }

    protected void SetUIState(bool state)
    {
        m_isUIOpen = state;

        if (state == true)
            m_uiOpenCount++;
        else
            m_uiOpenCount--;

        if (m_uiOpenCount == 1)
            OnAnyScreenUIOpen?.Invoke();
        else if (m_uiOpenCount == 0)
            OnNoScreenUIOpen?.Invoke();

    }

}
