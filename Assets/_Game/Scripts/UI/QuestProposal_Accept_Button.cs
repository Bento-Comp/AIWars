using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestProposal_Accept_Button : MonoBehaviour
{
    public static System.Action OnAcceptQuest_ButtonPressed;

    [SerializeField]
    private Button m_controlledButton = null;


    private void OnEnable()
    {
        m_controlledButton.onClick.AddListener(AcceptQuest_ButtonPressed);
    }

    private void OnDisable()
    {
        m_controlledButton.onClick.RemoveListener(AcceptQuest_ButtonPressed);
    }


    private void AcceptQuest_ButtonPressed()
    {
        OnAcceptQuest_ButtonPressed?.Invoke();
    }
}
