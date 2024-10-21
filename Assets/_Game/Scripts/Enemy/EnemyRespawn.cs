using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawn : MonoBehaviour
{
    public System.Action OnRespawn;
    public System.Action OnStartRespawnAnimation;

    [SerializeField]
    private EnemyHealth m_enemyHealth = null;

    [SerializeField]
    private Animator m_respawnAnimator = null;

    [SerializeField]
    private float m_respawnTime = 10f;

    [SerializeField]
    private float m_animationRespawnDuration = 1f;


    private Coroutine m_respawnCoroutine;
    private Coroutine m_animationRespawnCoroutine;


    private void OnEnable()
    {
        m_enemyHealth.OnDeath += OnDeath;
    }

    private void OnDisable()
    {
        m_enemyHealth.OnDeath -= OnDeath;
    }


    private void OnDeath()
    {
        if (m_respawnCoroutine != null)
            StopCoroutine(m_respawnCoroutine);

        m_respawnCoroutine = StartCoroutine(RespawnCoroutine());

        if (m_animationRespawnCoroutine != null)
            StopCoroutine(m_animationRespawnCoroutine);

        m_animationRespawnCoroutine = StartCoroutine(AnimationRespawnCoroutine());
    }

    private IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(m_respawnTime);
        OnRespawn?.Invoke();
    }

    private IEnumerator AnimationRespawnCoroutine()
    {
        float timeToWait = m_respawnTime - m_animationRespawnDuration;

        yield return new WaitForSeconds(timeToWait < 0 ? 0 : timeToWait);

        OnStartRespawnAnimation?.Invoke();

        LaunchRespawnAnimation();
    }

    private void LaunchRespawnAnimation()
    {
        m_respawnAnimator.SetTrigger("Appear");
    }
}
