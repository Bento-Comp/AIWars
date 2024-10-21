using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagFull_UI : MonoBehaviour
{
    [SerializeField]
    private GameObject m_UI;

    [SerializeField]
    private Animator m_animator = null;



    private void OnEnable()
    {
        PlayerGearCollector.OnBagFull += OnBagFull;
    }

    private void OnDisable()
    {
        PlayerGearCollector.OnBagFull -= OnBagFull;
    }

    private void Start()
    {
        m_UI.SetActive(false);
    }

    private void OnBagFull()
    {
        m_UI.SetActive(true);
        m_animator.SetTrigger("Appear");
    }


}
