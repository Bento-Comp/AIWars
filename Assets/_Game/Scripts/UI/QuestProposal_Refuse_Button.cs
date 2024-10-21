using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestProposal_Refuse_Button : MonoBehaviour
{
    public static System.Action OnRefuseQuest_ButtonPressed;

    [SerializeField]
    private Button m_controlledButton = null;


    private void OnEnable()
    {
        m_controlledButton.onClick.AddListener(RefuseQuest_ButtonPressed);
    }

    private void OnDisable()
    {
        m_controlledButton.onClick.RemoveListener(RefuseQuest_ButtonPressed);
    }


    private void RefuseQuest_ButtonPressed()
    {
        OnRefuseQuest_ButtonPressed?.Invoke();
    }
}
