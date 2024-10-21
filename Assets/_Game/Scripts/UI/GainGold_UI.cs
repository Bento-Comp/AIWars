using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GainGold_UI : MonoBehaviour
{
    [SerializeField]
    private GameObject m_UI = null;

    [SerializeField]
    private Animator m_animator = null;

    [SerializeField]
    private TMP_Text m_gainGoldText = null;

    [SerializeField]
    private float m_gainGoldAnimationTime = 2f;

    [SerializeField]
    private float m_gainedGoldFeedbackTime = 2f;

    private Coroutine m_gainGoldCoroutine;

    private void OnEnable()
    {
        Manager_Gold.OnGainGold += OnGainGold;
    }

    private void OnDisable()
    {
        Manager_Gold.OnGainGold -= OnGainGold;
    }

    private void Start()
    {
        m_UI.SetActive(false);
    }

    private void OnGainGold(float amountGained)
    {
        if (m_gainGoldCoroutine != null)
            StopCoroutine(m_gainGoldCoroutine);

        m_gainGoldCoroutine = StartCoroutine(GainGoldCoroutine(amountGained));

    }

    private IEnumerator GainGoldCoroutine(float amountGained)
    {
        m_UI.SetActive(true);

        if (m_animator != null)
            m_animator.SetTrigger("Appear");

        float gainStep = amountGained / m_gainGoldAnimationTime;
        float timer = 0f;
        float increasingGains = 0f;

        while (timer < m_gainGoldAnimationTime)
        {
            timer += Time.deltaTime;
            increasingGains += gainStep * Time.deltaTime;
            m_gainGoldText.text = "+" + increasingGains.ToString("F0");
            yield return new WaitForEndOfFrame();
        }

        m_gainGoldText.text = "+" + amountGained.ToString("F0");

        yield return new WaitForSeconds(m_gainedGoldFeedbackTime);

        m_UI.SetActive(false);
    }


}
