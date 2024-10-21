using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSession : MonoBehaviour
{

    public float m_levelTimer;
    public List<float> m_timerLevelList;


    private int m_levelLoaded;
    private bool m_isCurrentLevelLoaded = false;


    private void OnEnable()
    {
        PlayerXP.OnLevelUp += OnLevelUp;
        PlayerXP.OnBroadcastLevel += OnBroadcastLevel;
    }

    private void OnDisable()
    {
        PlayerXP.OnLevelUp -= OnLevelUp;
        PlayerXP.OnBroadcastLevel -= OnBroadcastLevel;
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause == true)
            SaveCurrentTimer();
    }

    private void OnApplicationQuit()
    {
        SaveCurrentTimer();
    }


    private void Update()
    {
        m_levelTimer += Time.deltaTime;
    }


    private void OnBroadcastLevel(int level)
    {
        if (m_isCurrentLevelLoaded == true)
            return;

        m_levelLoaded = level;

        m_isCurrentLevelLoaded = true;

        LoadCurrentTimer();
        LoadTimers();
    }

    private void OnLevelUp(int level)
    {
        m_timerLevelList.Add(m_levelTimer);
        SaveTimers();

        m_levelTimer = 0f;
        SaveCurrentTimer();
    }



    private void LoadTimers()
    {
        m_timerLevelList = new List<float>();

        for (int i = 0; i < m_levelLoaded - 1; i++)
        {
            if (PlayerPrefs.HasKey("TimerLevel" + i.ToString()))
                m_timerLevelList.Add(PlayerPrefs.GetFloat("TimerLevel" + i.ToString()));
        }
    }

    private void SaveTimers()
    {
        for (int i = 0; i < m_timerLevelList.Count; i++)
        {
            PlayerPrefs.SetFloat("TimerLevel" + i.ToString(), m_timerLevelList[i]);
        }
    }

    private void LoadCurrentTimer()
    {
        if (PlayerPrefs.HasKey("LevelTimer"))
            m_levelTimer = PlayerPrefs.GetFloat("LevelTimer");
    }

    private void SaveCurrentTimer()
    {
        PlayerPrefs.SetFloat("LevelTimer", m_levelTimer);
    }
}
