using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public System.Action OnInitialize;
    public System.Action OnDisableEnemy;


    [SerializeField]
    private EnemyHealth m_enemyHealth = null;

    [SerializeField]
    private EnemyRespawn m_enemyRespawn = null;

    [SerializeField]
    private Rigidbody m_body;

    [SerializeField]
    private List<GameObject> m_visualObjectList = null;

    [SerializeField]
    private List<Collider> m_colliderList = null;

    [SerializeField]
    private GameObject m_areaOfDetectionVisual = null;

    [SerializeField]
    private bool m_isAgressive = false;

    private bool m_isAlive;

    public bool IsAlive { get => m_isAlive; }
    public bool IsAgressive { get => m_isAgressive; }



    private void OnEnable()
    {
        m_enemyHealth.OnDeath += OnDeath;
        m_enemyRespawn.OnRespawn += OnRespawn;
    }

    private void OnDisable()
    {
        m_enemyHealth.OnDeath -= OnDeath;
        m_enemyRespawn.OnRespawn -= OnRespawn;
    }


    private void Start()
    {
        Initialize();
    }


    private void OnRespawn()
    {
        Initialize();
    }

    private void OnDeath()
    {
        DisableEnemy();
    }


    private void Initialize()
    {
        m_body.isKinematic = false;
        m_isAlive = true;
        OnInitialize?.Invoke();
        ToggleVisualObject(true);

        m_areaOfDetectionVisual.SetActive(m_isAgressive);
    }

    private void DisableEnemy()
    {
        m_body.isKinematic = true;
        m_isAlive = false;
        OnDisableEnemy?.Invoke();
        ToggleVisualObject(false);
    }


    private void ToggleVisualObject(bool state)
    {
        for (int i = 0; i < m_visualObjectList.Count; i++)
        {
            m_visualObjectList[i].SetActive(state);
            
        }

        for (int j = 0; j < m_colliderList.Count; j++)
        {
            m_colliderList[j].enabled = state;
        }
    }
}
