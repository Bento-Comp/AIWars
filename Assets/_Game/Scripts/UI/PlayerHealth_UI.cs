using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth_UI : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> m_healthImageList = null;



    private void OnEnable()
    {
        PlayerHealth.OnSendCurrentHealth += OnSendCurrentHealth;
    }

    private void OnDisable()
    {
        PlayerHealth.OnSendCurrentHealth -= OnSendCurrentHealth;
    }


    private void OnSendCurrentHealth(int currentHealth)
    {
        for (int i = 0; i < m_healthImageList.Count; i++)
        {
            m_healthImageList[i].SetActive(i < currentHealth);
        }
    }

}
