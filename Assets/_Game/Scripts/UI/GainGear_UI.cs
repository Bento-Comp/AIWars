using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GainGear_UI : MonoBehaviour
{
    [SerializeField]
    private GameObject m_UI = null;

    [SerializeField]
    private Animator m_animator = null;

    [SerializeField]
    private TMP_Text m_gainGearText = null;

    [SerializeField]
    private float m_gainGearAnimationTime = 2f;

    [SerializeField]
    private float m_gainedGearFeedbackTime = 2f;


    private float m_amountGainedSpeedStep;
    private float m_targetAmountToDisplay;
    private float m_currentAmountDisplaying;
    private float m_closeUITimer;
    private bool m_isGainingGear;
    private bool m_isGainGearUIActive;

    private void OnEnable()
    {
        PlayerGearBag.OnGainGear += OnGainGear;
    }

    private void OnDisable()
    {
        PlayerGearBag.OnGainGear -= OnGainGear;
    }

    private void Start()
    {
        m_UI.SetActive(false);
        m_isGainGearUIActive = false;
        m_isGainingGear = false;
    }

    private void Update()
    {
        if (m_currentAmountDisplaying < m_targetAmountToDisplay)
        {
            m_currentAmountDisplaying += m_amountGainedSpeedStep * Time.deltaTime;
            m_gainGearText.text = "+" + m_currentAmountDisplaying.ToString("F0");

            if (m_currentAmountDisplaying > m_targetAmountToDisplay)
            {
                m_currentAmountDisplaying = m_targetAmountToDisplay;
                m_gainGearText.text = "+" + m_currentAmountDisplaying.ToString("F0");
                m_isGainingGear = false;
                m_closeUITimer = 0f;
            }
        }


        if (m_isGainGearUIActive == true && m_isGainingGear == false)
        {
            m_closeUITimer += Time.deltaTime;

            if (m_closeUITimer > m_gainedGearFeedbackTime)
            {
                m_isGainGearUIActive = false;
                m_UI.SetActive(false);
            }
        }
    }


    private void OnGainGear(float amountGained)
    {
        if (m_isGainGearUIActive == false)
        {
            m_UI.SetActive(true);
            m_isGainingGear = true;
            m_targetAmountToDisplay = 0f;
            m_currentAmountDisplaying = 0f;
            m_animator.SetTrigger("Appear");
            m_isGainGearUIActive = true;
        }

        m_targetAmountToDisplay += amountGained;
        m_amountGainedSpeedStep = (m_targetAmountToDisplay - m_currentAmountDisplaying) / m_gainGearAnimationTime;

    }


}
