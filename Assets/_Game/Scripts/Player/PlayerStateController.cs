using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    public static System.Action OnPlayerDeath;
    public static System.Action OnPlayerAlive;

    [SerializeField]
    private List<GameObject> m_objectDependingOnStateList = null;


    private bool m_isAlive;

    public bool IsAlive { get => m_isAlive; }


    private void OnEnable()
    {
        PlayerHealth.OnPlayerDeath += DisablePlayer;
        PlayerRespawn.OnPlayerRespawn += EnablePlayer;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDeath -= DisablePlayer;
        PlayerRespawn.OnPlayerRespawn -= EnablePlayer;
    }


    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        EnablePlayer();
    }

    private void EnablePlayer()
    {
        m_isAlive = true;

        for (int i = 0; i < m_objectDependingOnStateList.Count; i++)
        {
            m_objectDependingOnStateList[i].SetActive(true);
        }

        OnPlayerAlive?.Invoke();
    }

    private void DisablePlayer()
    {
        m_isAlive = false;

        for (int i = 0; i < m_objectDependingOnStateList.Count; i++)
        {
            m_objectDependingOnStateList[i].SetActive(false);
        }

        OnPlayerDeath?.Invoke();
    }


}
