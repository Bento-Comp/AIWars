using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageScreen_UI : MonoBehaviour
{
    [SerializeField]
    private Image m_image = null;

    [SerializeField]
    private float m_damageAnimationTime = 0.5f;


    private Coroutine m_damageAnimationCoroutine;
    private Color m_colorBuffer;
    private float m_timer;


    private void OnEnable()
    {
        PlayerHealth.OnPlayerTakeDamage += OnPlayerTakeDamage;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerTakeDamage -= OnPlayerTakeDamage;
    }

    private void Start()
    {
        m_colorBuffer = m_image.color;
    }


    private void OnPlayerTakeDamage()
    {
        if (m_damageAnimationCoroutine != null)
            StopCoroutine(m_damageAnimationCoroutine);

        m_damageAnimationCoroutine = StartCoroutine(DamageScreenAnimationCoroutine());
    }


    private IEnumerator DamageScreenAnimationCoroutine()
    {
        m_timer = 0f;


        while (m_timer < m_damageAnimationTime / 2f)
        {
            m_timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();

            m_colorBuffer.a = Mathf.Clamp01(m_timer / m_damageAnimationTime);

            m_image.color = m_colorBuffer;
        }

        m_timer = m_damageAnimationTime / 2f;

        while (m_timer > 0f)
        {
            m_timer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();

            m_colorBuffer.a = Mathf.Clamp01(m_timer / m_damageAnimationTime);

            m_image.color = m_colorBuffer;
        }
    }
}
