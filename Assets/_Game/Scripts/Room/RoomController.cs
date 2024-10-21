using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoomController : MonoBehaviour
{
    public static System.Action<Collider, RoomController, Vector3, Bounds> OnSendRoomInfoToEnemy;
    public static System.Action<int> OnSendLevelRequiredToOpenRoom;

    [SerializeField]
    private Bounds m_roomBounds;

    [SerializeField]
    private int m_playerLevelNeededToUnlockRoom = 1;

    [SerializeField]
    private List<TMP_Text> m_levelIndicatorTextList = null;

    [SerializeField]
    private List<Animator> m_animatorList = null;

    [SerializeField]
    private LayerMask m_enemyLayer = 0;

    private Collider[] m_enemyColliderArray;
    private bool m_isDoorOpen;

    public Bounds RoomBounds { get => m_roomBounds; }

    private void OnEnable()
    {
        PlayerXP.OnLevelUp += CheckDoorOpenCondition;
        PlayerXP.OnBroadcastLevel += CheckDoorOpenCondition;
    }

    private void OnDisable()
    {
        PlayerXP.OnLevelUp -= CheckDoorOpenCondition;
        PlayerXP.OnBroadcastLevel -= CheckDoorOpenCondition;
    }


    private void Start()
    {
        OnSendLevelRequiredToOpenRoom?.Invoke(m_playerLevelNeededToUnlockRoom);

        m_enemyColliderArray = Physics.OverlapBox(transform.position + m_roomBounds.center, m_roomBounds.extents, Quaternion.identity, m_enemyLayer);
        
        for (int i = 0; i < m_enemyColliderArray.Length; i++)
        {
            OnSendRoomInfoToEnemy?.Invoke(m_enemyColliderArray[i], this, transform.position, m_roomBounds);
        }
    }

    private void CheckDoorOpenCondition(int playerLevel)
    {
        if (m_isDoorOpen == true)
            return;

        if (playerLevel >= m_playerLevelNeededToUnlockRoom)
            OpenDoor();
        else
            CloseDoor();
    }

    private void OpenDoor()
    {
        m_isDoorOpen = true;

        for (int i = 0; i < m_levelIndicatorTextList.Count; i++)
        {
            m_levelIndicatorTextList[i].gameObject.SetActive(!m_isDoorOpen);
        }

        for (int i = 0; i < m_animatorList.Count; i++)
        {
            m_animatorList[i].SetBool("IsOpen", m_isDoorOpen);
        }
    }

    private void CloseDoor()
    {
        m_isDoorOpen = false;

        for (int i = 0; i < m_levelIndicatorTextList.Count; i++)
        {
            m_levelIndicatorTextList[i].gameObject.SetActive(!m_isDoorOpen);
            m_levelIndicatorTextList[i].text = m_playerLevelNeededToUnlockRoom.ToString();
        }

        for (int i = 0; i < m_animatorList.Count; i++)
        {
            m_animatorList[i].SetBool("IsOpen", m_isDoorOpen);
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position + m_roomBounds.center, m_roomBounds.size);
    }

}
