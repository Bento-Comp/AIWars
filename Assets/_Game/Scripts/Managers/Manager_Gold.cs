using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Gold : MonoBehaviour
{
    public static System.Action<float> OnGainGold;
    public static System.Action<float> OnBroadcastGoldHeld;

    public static Manager_Gold Instance;

    private string m_goldHeldKey = "goldHeld";
    private float m_currentGold;

    public float CurrentGold { get => m_currentGold; }


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    private void OnEnable()
    {
        PlayerStat.OnUpgradePurchased += OnUpgradePurchased;
        PlayerGearBag.OnConvertGearToGold += OnConvertGearToGold;

        LevelDesignTools.OnGainGold += LevelDesignTools_OnGainGold;

        QuestReward.OnGiveReward += OnGiveReward;
    }

    private void OnDisable()
    {
        PlayerStat.OnUpgradePurchased -= OnUpgradePurchased;
        PlayerGearBag.OnConvertGearToGold -= OnConvertGearToGold;

        LevelDesignTools.OnGainGold -= LevelDesignTools_OnGainGold;

        QuestReward.OnGiveReward -= OnGiveReward;
    }

    private void Start()
    {
        Initialize();
    }


    private void Initialize()
    {
        LoadGoldHeld();
    }

    private void LoadGoldHeld()
    {
        if (PlayerPrefs.HasKey(m_goldHeldKey) == false)
        {
            m_currentGold = 0;
            SaveGoldHeld();
        }
        else
        {
            m_currentGold = PlayerPrefs.GetFloat(m_goldHeldKey);
        }

        OnBroadcastGoldHeld?.Invoke(m_currentGold);
    }

    private void SaveGoldHeld()
    {
        PlayerPrefs.SetFloat(m_goldHeldKey, m_currentGold);
    }


    private void OnGiveReward(QuestReward questReward)
    {
        if (questReward.m_questRewardType == QuestRewardType.Gold)
            GainGold(questReward.m_amount);
    }

    private void LevelDesignTools_OnGainGold(float goldToGain)
    {
        GainGold(goldToGain);
    }

    private void OnConvertGearToGold(float gearConvertedToGold)
    {
        GainGold(gearConvertedToGold);
    }

    private void OnUpgradePurchased(StatType statType, float upgradeCost)
    {
        SpendGold(upgradeCost);
    }


    private void GainGold(float amount)
    {
        m_currentGold += amount;
        OnGainGold?.Invoke(amount);
        OnBroadcastGoldHeld?.Invoke(m_currentGold);
        SaveGoldHeld();
    }

    private void SpendGold(float amount)
    {
        if (CanPurchase(amount) == false)
            return;

        m_currentGold -= amount;

        OnBroadcastGoldHeld?.Invoke(m_currentGold);

        SaveGoldHeld();
    }

    public bool CanPurchase(float amountToSpend)
    {
        return (m_currentGold >= amountToSpend);
    }

}
