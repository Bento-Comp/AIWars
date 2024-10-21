using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniButton;

namespace UniButton
{
    [AddComponentMenu("UniButton/TouchButtonController_Stick")]
    public class TouchButtonController_Stick : MonoBehaviour
    {
        public static System.Action<Vector2> OnSendTouchStartPosition;
        public static System.Action<Vector2> OnSendStickVector;

        public UniButton.TouchButtonController touchButtonController;

        public float amplitudeMax = 0.5f;

        public bool clampToAmplitude = true;

        public bool keepTightStick = true;

        public bool gizmo_enable;

        public float gizmo_knobSize_centimeters = 0.15f;

        public bool squareStick;

        Button button;

        public Vector2 Stick { get; private set; }

        public bool Pressed => touchButtonController.controlledButton.Pressed;

        public Vector2 debug;

        public void MoveStickCenter(Vector2 movementInStickPercent)
        {
            Vector2 movementInPixel = movementInStickPercent * amplitudeMax * TouchUtility.CentimetersToPixel;

            touchButtonController.StartTouchPosition += movementInPixel;
        }

        void Awake()
        {
            button = touchButtonController.controlledButton;
        }

        private void OnEnable()
        {
            ShowQuestMenu_ButtonUI.OnShowQuestUI_ButtonPressed += ResetStick;
            ShowUpgradeMenu_ButtonUI.OnShowUpgradeUI_ButtonPressed += ResetStick;
        }

        private void OnDisable()
        {
            ShowQuestMenu_ButtonUI.OnShowQuestUI_ButtonPressed -= ResetStick;
            ShowUpgradeMenu_ButtonUI.OnShowUpgradeUI_ButtonPressed -= ResetStick;
        }

        void Update()
        {
            UpdateStick();
        }

        private void ResetStick()
        {
            Stick = Vector2.zero;
            OnSendTouchStartPosition?.Invoke(new Vector2(Screen.width / 2f, Screen.height / 5f));
            OnSendStickVector?.Invoke(Stick);
        }

        void UpdateStick()
        {
            if (button.Pressed == false)
            {
                Stick = Vector2.zero;
                OnSendTouchStartPosition?.Invoke(new Vector2(Screen.width / 2f, Screen.height / 5f));
                OnSendStickVector?.Invoke(Stick);
                return;
            }

            // Keep a tight stick
            if (keepTightStick)
            {
                Vector2 currentStickDirection = touchButtonController.TouchMovementFromStart_Centimeter.normalized;
                float currentStickDistance = touchButtonController.TouchMovementFromStart_Centimeter.magnitude;
                float maxStartStickDistance = amplitudeMax;
                if (currentStickDistance >= maxStartStickDistance)
                {
                    touchButtonController.StartTouchPosition = touchButtonController.CurrentTouchPosition
                        - currentStickDirection * maxStartStickDistance * TouchUtility.CentimetersToPixel;
                }
            }

            // Compute stick
            Vector2 stickVector = touchButtonController.TouchMovementFromStart_Centimeter;

            float amplitude = stickVector.magnitude;

            if (amplitude <= 0.0f)
            {
                stickVector = Vector2.zero;
            }
            else if (clampToAmplitude)
            {
                if (squareStick)
                {
                    stickVector /= amplitudeMax;
                    stickVector.x = Mathf.Clamp(stickVector.x, -1.0f, 1.0f);
                    stickVector.y = Mathf.Clamp(stickVector.y, -1.0f, 1.0f);
                }
                else
                {
                    if (amplitude >= amplitudeMax)
                    {
                        stickVector /= amplitude;
                    }
                    else
                    {
                        stickVector /= amplitudeMax;
                    }
                }
            }
            else
            {
                stickVector /= amplitudeMax;
            }

            Stick = stickVector;

            OnSendTouchStartPosition?.Invoke(touchButtonController.StartTouchPosition);
            OnSendStickVector?.Invoke(Stick);
        }

        void OnDrawGizmos()
        {
            Camera mainCamera = Camera.main;

            if (mainCamera == null)
                return;

            if (touchButtonController == null)
                return;

            float depth = Mathf.Lerp(mainCamera.nearClipPlane, mainCamera.farClipPlane, 0.5f);

            float pixelToWorld = (mainCamera.ScreenToWorldPoint(new Vector3(0.0f, 1.0f, depth)) - mainCamera.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, depth))).magnitude;

            Vector3 startTouch = touchButtonController.StartTouchPosition;
            Vector3 currentTouch = touchButtonController.CurrentTouchPosition;

            Vector3 start = mainCamera.ScreenToWorldPoint(new Vector3(touchButtonController.StartTouchPosition.x, touchButtonController.StartTouchPosition.y, depth));
            Vector3 current = mainCamera.ScreenToWorldPoint(new Vector3(touchButtonController.CurrentTouchPosition.x, touchButtonController.CurrentTouchPosition.y, depth));

            float amplitudeWorld = amplitudeMax * TouchUtility.CentimetersToPixel * pixelToWorld;

            Vector3 stickEnd = start + (Vector3)Stick * amplitudeWorld;

            Gizmos.color = Color.cyan;

            Gizmos.DrawWireSphere(start, amplitudeWorld);

            float distance = (current - start).magnitude;
            float epsilon = 1.0f * pixelToWorld;
            if (distance >= (amplitudeWorld - epsilon))
            {
                Vector3 intercept = (current - start).normalized * amplitudeWorld + start;

                Gizmos.DrawLine(start, intercept);

                Gizmos.color = Color.red;

                Gizmos.DrawLine(intercept, current);
            }
            else
            {
                Gizmos.DrawLine(start, current);
            }

            Gizmos.DrawWireSphere(current, gizmo_knobSize_centimeters * TouchUtility.CentimetersToPixel * pixelToWorld);

            Gizmos.color = Color.white;

            Gizmos.DrawLine(start, stickEnd);
            Gizmos.DrawWireSphere(stickEnd, gizmo_knobSize_centimeters * 0.5f * TouchUtility.CentimetersToPixel * pixelToWorld);
        }
    }
}