using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRobotAnimatorController : MonoBehaviour
{
    [SerializeField]
    private Animator m_animator = null;

    [SerializeField]
    private EnemyMovement m_enemyMovement = null;

    [SerializeField]
    private EnemyHealth m_enemyHealth = null;



    private void OnEnable()
    {
        m_enemyHealth.OnGetHit += OnGetHit;
    }

    private void OnDisable()
    {
        m_enemyHealth.OnGetHit -= OnGetHit;
    }

    private void Update()
    {
        if (m_enemyMovement.IsMoving && !m_animator.GetBool("IsMoving"))
            m_animator.SetBool("IsMoving", true);
        else if (!m_enemyMovement.IsMoving && m_animator.GetBool("IsMoving"))
            m_animator.SetBool("IsMoving", false);
    }


    private void OnGetHit()
    {
        m_animator.SetTrigger("GetHit");
    }

}
