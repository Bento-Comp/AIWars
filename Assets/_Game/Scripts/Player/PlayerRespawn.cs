using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public static System.Action OnPlayerRespawn;

    [SerializeField]
    private Rigidbody m_body = null;

    [SerializeField]
    private ColliderObjectDetector m_machineGearToGoldDetector = null;

    [SerializeField]
    private float m_delayAfterDeathBeforeRespawn = 2f;

    [SerializeField]
    private Vector3 m_respawnOffsetFromMachine = Vector3.back;

    private Coroutine m_respawnCoroutine;
    private Vector3 m_respawnPosition;


    private void OnEnable()
    {
        PlayerStateController.OnPlayerDeath += OnPlayerDeath;

        m_machineGearToGoldDetector.OnObjectDetected += OnReachMachineGearToGold;
    }

    private void OnDisable()
    {
        PlayerStateController.OnPlayerDeath -= OnPlayerDeath;

        m_machineGearToGoldDetector.OnObjectDetected -= OnReachMachineGearToGold;
    }



    private void Start()
    {
        Initialize();
    }


    private void Initialize()
    {
        LoadRespawnPosition();
        m_body.position = m_respawnPosition;
    }


    private void OnReachMachineGearToGold(GameObject colliderObject)
    {
        m_respawnPosition = colliderObject.transform.position + m_respawnOffsetFromMachine;
        SaveRespawnPosition();
    }


    private void SaveRespawnPosition()
    {
        PlayerPrefs.SetFloat("RespawnPositionX", m_respawnPosition.x);
        PlayerPrefs.SetFloat("RespawnPositionZ", m_respawnPosition.z);
    }

    private void LoadRespawnPosition()
    {
        if(PlayerPrefs.HasKey("RespawnPositionX") && PlayerPrefs.HasKey("RespawnPositionZ"))
        {
            m_respawnPosition.x = PlayerPrefs.GetFloat("RespawnPositionX");
            m_respawnPosition.z = PlayerPrefs.GetFloat("RespawnPositionZ");
        }
        else
        {
            m_respawnPosition = m_body.position;
            SaveRespawnPosition();
        }

    }

    private void OnPlayerDeath()
    {
        if (m_respawnCoroutine != null)
            StopCoroutine(m_respawnCoroutine);

        m_respawnCoroutine = StartCoroutine(RespawnCoroutine());
    }

    private IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(m_delayAfterDeathBeforeRespawn);
        Respawn();
    }

    private void Respawn()
    {
        m_body.position = m_respawnPosition;

        OnPlayerRespawn?.Invoke();
    }

}
