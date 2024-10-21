using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelUp_UI : MonoBehaviour
{
    [SerializeField]
    private GameObject m_UI;

    [SerializeField]
    private Animator m_animator = null;

    [SerializeField]
    private TMP_Text m_levelUpText = null;


    private int m_levelNeededToOpenLastRoom;


    private void OnEnable()
    {
        PlayerXP.OnLevelUp += OnLevelUp;
        RoomController.OnSendLevelRequiredToOpenRoom += OnSendLevelRequiredToOpenRoom;
    }

    private void OnDisable()
    {
        PlayerXP.OnLevelUp -= OnLevelUp;
        RoomController.OnSendLevelRequiredToOpenRoom -= OnSendLevelRequiredToOpenRoom;
    }

    private void Start()
    {
        m_UI.SetActive(false);
    }

    private void OnSendLevelRequiredToOpenRoom(int levelRequired)
    {
        if (m_levelNeededToOpenLastRoom < levelRequired)
            m_levelNeededToOpenLastRoom = levelRequired;
    }

    private void OnLevelUp(int playerLevel)
    {
        m_UI.SetActive(true);

        if (playerLevel <= m_levelNeededToOpenLastRoom)
            m_levelUpText.text = "Level Up! \nRoom " + playerLevel.ToString() + " Unlocked";
        else
            m_levelUpText.text = "Level Up!";

        m_animator.SetTrigger("Appear");
    }


}
