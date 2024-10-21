using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Gear_UI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text m_gearText = null;

    [SerializeField]
    private Slider m_bagFillSlider = null;


    private void OnEnable()
    {
        PlayerGearBag.OnBroadcastGearPosessed += OnBroadcastGearPosessed;
    }

    private void OnDisable()
    {
        PlayerGearBag.OnBroadcastGearPosessed -= OnBroadcastGearPosessed;
    }


    private void OnBroadcastGearPosessed(float gearCount, float bagSize)
    {
        m_gearText.text = "Bag (" + gearCount.ToString("F0") + "/" + bagSize.ToString("F0") + ")";
        m_bagFillSlider.value = gearCount / bagSize;
    }


}
