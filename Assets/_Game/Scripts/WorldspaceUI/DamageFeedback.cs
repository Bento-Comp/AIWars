using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageFeedback : MonoBehaviour
{
    [SerializeField]
    private GameObject m_rootObject = null;

    [SerializeField]
    private TMP_Text m_damageText = null;



    private void OnEnable()
    {
        DamageFeedback_Manager.OnDamageFeedbackCreated += OnDamageFeedbackCreated;
    }

    private void OnDisable()
    {
        DamageFeedback_Manager.OnDamageFeedbackCreated -= OnDamageFeedbackCreated;
    }

    private void OnDamageFeedbackCreated(GameObject damageFeedbackObjectReference, float damage)
    {
        if (m_rootObject != damageFeedbackObjectReference)
            return;

        UpdateDamageText(damage.ToString("F1"));
    }

    private void UpdateDamageText(string text)
    {
        m_damageText.text = text;
    }



}
