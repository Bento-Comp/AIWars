using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera m_vCam = null;

    [SerializeField]
    private float m_shakeDuration = 0.2f;

    [SerializeField]
    private float m_amplitude = 1.2f;

    [SerializeField]
    private float m_frequency = 0.25f;


    private CinemachineBasicMultiChannelPerlin m_noiseProfile;
    private Coroutine m_cameraShakeCoroutine;


    private void Awake()
    {
        m_noiseProfile = m_vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        m_noiseProfile.m_AmplitudeGain = 0;
        m_noiseProfile.m_FrequencyGain = 0;
    }

    private void OnEnable()
    {
        PlayerHealth.OnPlayerTakeDamage += OnPlayerTakeDamage;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerTakeDamage -= OnPlayerTakeDamage;
    }


    private void OnPlayerTakeDamage()
    {
        if (m_cameraShakeCoroutine != null)
            StopCoroutine(m_cameraShakeCoroutine);

        m_cameraShakeCoroutine = StartCoroutine(CameraShakeCoroutine());
    }


    private IEnumerator CameraShakeCoroutine()
    {
        m_noiseProfile.m_AmplitudeGain = m_amplitude;
        m_noiseProfile.m_FrequencyGain = m_frequency;

        yield return new WaitForSeconds(m_shakeDuration);

        m_noiseProfile.m_AmplitudeGain = 0;
        m_noiseProfile.m_FrequencyGain = 0;
    }
}
