using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gold_UI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text m_goldText = null;


    private void OnEnable()
    {
        Manager_Gold.OnBroadcastGoldHeld+= OnBroadcastGoldHeld;
    }

    private void OnDisable()
    {
        Manager_Gold.OnBroadcastGoldHeld -= OnBroadcastGoldHeld;
    }


    private void OnBroadcastGoldHeld(float goldAmount)
    {
        m_goldText.text = goldAmount.ToString("F0");
    }


}
