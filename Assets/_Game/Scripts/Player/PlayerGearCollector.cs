using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGearCollector : MonoBehaviour
{
    //Gameobject : gear collected reference; Gameobject : player gear collector reference
    public static System.Action<GameObject, GameObject> OnCollectGear;
    public static System.Action OnBagFull;


    [SerializeField]
    private ColliderObjectDetector m_gearCollectableDetector = null;

    [SerializeField]
    private PlayerGearBag m_playerGearBag = null;



    private void OnEnable()
    {
        m_gearCollectableDetector.OnObjectDetected += OnObjectDetected;
    }

    private void OnDisable()
    {
        m_gearCollectableDetector.OnObjectDetected -= OnObjectDetected;
    }


    private void OnObjectDetected(GameObject colliderObject)
    {
        if (m_playerGearBag.CanCollectGear == false)
        {
            OnBagFull?.Invoke();
            return;
        }

        OnCollectGear?.Invoke(colliderObject, m_gearCollectableDetector.gameObject);
    }


}
