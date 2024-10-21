using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGearBag : MonoBehaviour
{
    public static System.Action<GameObject> OnSendLastMachineReached;
    public static System.Action<float, float> OnBroadcastGearPosessed;
    public static System.Action<float> OnConvertGearToGold;
    public static System.Action<float> OnGainGear;
    

    [SerializeField]
    private PlayerStat_BagSize m_bagSizeStat = null;


    [SerializeField]
    private ColliderObjectDetector m_machineGearToGoldDetector = null;


    private string m_collectedGearKey = "collectedGear";
    private float m_collectedGear;
    private float m_bagSize;


    public bool CanCollectGear { get => m_collectedGear < m_bagSize; }
    public float CollectedGear { get => m_collectedGear; }


    private void OnEnable()
    {
        GearCollectable.OnSendCollectedGearValue += OnSendCollectedGearValue;
        m_bagSizeStat.OnStatChange += OnStatChange;

        m_machineGearToGoldDetector.OnObjectDetected += OnMachineGearToGoldDetected;
    }

    private void OnDisable()
    {
        GearCollectable.OnSendCollectedGearValue -= OnSendCollectedGearValue;
        m_bagSizeStat.OnStatChange -= OnStatChange;

        m_machineGearToGoldDetector.OnObjectDetected -= OnMachineGearToGoldDetected;
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        LoadGearCollected();
    }


    private void OnMachineGearToGoldDetected(GameObject colliderObject)
    {
        if (m_collectedGear == 0)
            return;

        OnSendLastMachineReached?.Invoke(colliderObject);
        OnConvertGearToGold?.Invoke(m_collectedGear);
        m_collectedGear = 0f;
        OnBroadcastGearPosessed?.Invoke(m_collectedGear, m_bagSize);
        SaveGearCollected();
    }

    private void LoadGearCollected()
    {
        m_bagSize = (int)m_bagSizeStat.GetStatValue();

        if (PlayerPrefs.HasKey(m_collectedGearKey) == false)
        {
            m_collectedGear = 0;
            SaveGearCollected();
        }
        else
        {
            m_collectedGear = PlayerPrefs.GetFloat(m_collectedGearKey);
        }


        OnBroadcastGearPosessed?.Invoke(m_collectedGear, m_bagSize);
    }

    private void SaveGearCollected()
    {
        PlayerPrefs.SetFloat(m_collectedGearKey, m_collectedGear);
    }

    private void OnStatChange()
    {
        m_bagSize = (int)m_bagSizeStat.GetStatValue();
        OnBroadcastGearPosessed?.Invoke(m_collectedGear, m_bagSize);
    }

    private void OnSendCollectedGearValue(float gearValue)
    {
        if(m_collectedGear >= m_bagSize)
        {
            return;
        }

        m_collectedGear += gearValue;

        if (m_collectedGear > m_bagSize)
        {
            m_collectedGear = m_bagSize;
        }

        SaveGearCollected();
        OnGainGear?.Invoke(gearValue);
        OnBroadcastGearPosessed?.Invoke(m_collectedGear, m_bagSize);
    }


}
