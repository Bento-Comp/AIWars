using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputsController : MonoBehaviour
{
    

    [SerializeField]
    private GameObject m_virtualStick = null;


    private void OnEnable()
    {
        Screen_UI.OnAnyScreenUIOpen += OnAnyScreenUIOpen;
        Screen_UI.OnNoScreenUIOpen+= OnNoScreenUIOpen;
    }

    private void OnDisable()
    {
        Screen_UI.OnAnyScreenUIOpen -= OnAnyScreenUIOpen;
        Screen_UI.OnNoScreenUIOpen -= OnNoScreenUIOpen;
    }


    private void Start()
    {
        EnableVirtualStickControls();
    }


    private void OnAnyScreenUIOpen()
    {
        DisableVirtualStickControls();
    }

    private void OnNoScreenUIOpen()
    {
        EnableVirtualStickControls();
    }


    private void EnableVirtualStickControls()
    {
        m_virtualStick.SetActive(true);
    }

    private void DisableVirtualStickControls()
    {
        m_virtualStick.SetActive(false);
    }


}
