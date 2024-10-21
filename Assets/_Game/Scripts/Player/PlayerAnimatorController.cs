using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement m_playerMovement = null;

    [SerializeField]
    private Animator m_animator = null;

    private Vector3 m_blendTreeVelocityPercent;


    private void OnEnable()
    {
        PlayerStateController.OnPlayerDeath += OnPlayerDeath;
        PlayerStateController.OnPlayerAlive += OnPlayerAlive;
    }

    private void OnDisable()
    {
        PlayerStateController.OnPlayerDeath -= OnPlayerDeath;
        PlayerStateController.OnPlayerAlive -= OnPlayerAlive;
    }


    private void Update()
    {
        UpdateBlendParameters();
    }


    private void OnPlayerAlive()
    {
        m_animator.SetBool("IsDead", false);
    }

    private void OnPlayerDeath()
    {
        m_animator.SetBool("IsDead", true);
    }


    private void UpdateBlendParameters()
    {
        m_blendTreeVelocityPercent = m_playerMovement.transform.InverseTransformDirection(m_playerMovement.DesiredMovement);

        m_animator.SetFloat("VelocityX", m_blendTreeVelocityPercent.x);
        m_animator.SetFloat("VelocityZ", m_blendTreeVelocityPercent.z);
    }

}
