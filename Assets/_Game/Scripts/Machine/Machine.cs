using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour
{

    [SerializeField]
    private Collider m_triggerCollider = null;

    [SerializeField]
    private ParticleSystem m_gainGoldFx = null;

    [SerializeField]
    private float m_fxDuration = 1.5f;


    private Coroutine m_gainGoldFxCoroutine;
    private bool m_isLastVisitedMachine;


    private void OnEnable()
    {
        PlayerGearBag.OnSendLastMachineReached += OnSendLastMachineReached;
        Manager_Gold.OnGainGold += OnGainGold;
    }

    private void OnDisable()
    {
        PlayerGearBag.OnSendLastMachineReached -= OnSendLastMachineReached;
        Manager_Gold.OnGainGold -= OnGainGold;
    }

    private void Start()
    {
        m_gainGoldFx.Stop();
    }

    private void OnSendLastMachineReached(GameObject colliderMachine)
    {
        m_isLastVisitedMachine = (m_triggerCollider.gameObject == colliderMachine);
    }

    private void OnGainGold(float gainedGold)
    {
        if (m_isLastVisitedMachine)
        {
            if (m_gainGoldFxCoroutine != null)
                StopCoroutine(m_gainGoldFxCoroutine);

            m_gainGoldFxCoroutine = StartCoroutine(ShowGainGoldFxCoroutine());
        }
    }


    private IEnumerator ShowGainGoldFxCoroutine()
    {
        m_gainGoldFx.Play();
        yield return new WaitForSeconds(m_fxDuration);
        m_gainGoldFx.Stop();
    }

}
