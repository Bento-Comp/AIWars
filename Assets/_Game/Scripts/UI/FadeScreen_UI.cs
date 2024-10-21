using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreen_UI : MonoBehaviour
{
    [SerializeField]
    private Animator m_animator = null;



    private void OnEnable()
    {
        PlayerStateController.OnPlayerDeath += OnPlayerDeath;
        PlayerRespawn.OnPlayerRespawn += OnPlayerRespawn;
    }

    private void OnDisable()
    {
        PlayerStateController.OnPlayerDeath -= OnPlayerDeath;
        PlayerRespawn.OnPlayerRespawn -= OnPlayerRespawn;
    }


    private void OnPlayerDeath()
    {
        m_animator.SetTrigger("FadeOut");
    }


    private void OnPlayerRespawn()
    {
        m_animator.SetTrigger("FadeIn");
    }

}
