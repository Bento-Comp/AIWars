using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualStick_UI : MonoBehaviour
{
    [SerializeField]
    private RectTransform m_stickCircleRectTransform = null;

    [SerializeField]
    private RectTransform m_stickKnobRectTransform = null;

    [SerializeField]
    private float m_visualStickAmplitude = 125f;


    private void OnEnable()
    {
        UniButton.TouchButtonController_Stick.OnSendTouchStartPosition += OnSendTouchStartPosition;
        UniButton.TouchButtonController_Stick.OnSendStickVector += OnSendStickVector;
    }

    private void OnDisable()
    {
        UniButton.TouchButtonController_Stick.OnSendTouchStartPosition -= OnSendTouchStartPosition;
        UniButton.TouchButtonController_Stick.OnSendStickVector -= OnSendStickVector;
    }



    private void OnSendTouchStartPosition(Vector2 touchStartPosition)
    {
        m_stickCircleRectTransform.anchoredPosition = touchStartPosition;
    }

    private void OnSendStickVector(Vector2 stickVector)
    {
        m_stickKnobRectTransform.anchoredPosition = stickVector * m_visualStickAmplitude;
    }

}
