using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    BagSize,
    MovementSpeed,
    ShootDamage,
    ShootRange,
    ShootSpeed
}

public class PlayerStat : MonoBehaviour
{
    public System.Action OnStatChange;
    public static System.Action<StatType, float> OnUpgradePurchased;
    // int : stat level, float : stat cost, float : current level stat value, float : next level stat value, bool : can purchase stat upgrade, bool : is stat maxed out
    public static System.Action<StatType, int, float, float, float, bool, bool> OnSendStatInfo;


    [SerializeField]
    protected PlayerStatsChart_ScriptableObject m_playerStat = null;

    [SerializeField]
    private StatType m_statType;

    [SerializeField]
    private int m_statMaxLevel = 10;

    protected int m_statCurrentLevel;

    private string m_playerStatLevelKey { get => m_statType.ToString(); }

    public int StatCurrentLevel { get => m_statCurrentLevel; }
    private bool IsStatMaxxed { get => m_statCurrentLevel >= m_statMaxLevel; }


    private void OnEnable()
    {
        StatSlot_UI.OnAskStatInfo += OnAskStatInfo;
        StatSlot_UI.OnUpgradeStat_ButtonPressed += OnUpgradeStat_ButtonPressed;

        Manager_Gold.OnBroadcastGoldHeld += OnBroadcastGoldHeld;
    }

    private void OnDisable()
    {
        StatSlot_UI.OnAskStatInfo -= OnAskStatInfo;
        StatSlot_UI.OnUpgradeStat_ButtonPressed -= OnUpgradeStat_ButtonPressed;

        Manager_Gold.OnBroadcastGoldHeld -= OnBroadcastGoldHeld;
    }


    private void Start()
    {
        Initialize();
    }


    private void Initialize()
    {
        LoadLevelAndXp();
    }

    private void LoadLevelAndXp()
    {
        if (PlayerPrefs.HasKey(m_playerStatLevelKey) == false)
        {
            m_statCurrentLevel = 1;
            SaveStat();
        }
        else
        {
            m_statCurrentLevel = PlayerPrefs.GetInt(m_playerStatLevelKey);
        }

        OnStatChange?.Invoke();
    }

    private void SaveStat()
    {
        PlayerPrefs.SetInt(m_playerStatLevelKey, m_statCurrentLevel);
    }

    private void OnBroadcastGoldHeld(float goldHeld)
    {
        OnSendStatInfo?.Invoke(m_statType, m_statCurrentLevel, GetStatCost(), GetStatValue(), GetStatValueNextLevel(), CanPurchaseUpgrade(), IsStatMaxxed);
    }

    private void OnAskStatInfo(StatType statType)
    {
        if (statType != m_statType)
            return;

        OnSendStatInfo?.Invoke(statType, m_statCurrentLevel, GetStatCost(), GetStatValue(), GetStatValueNextLevel(), CanPurchaseUpgrade(), IsStatMaxxed);
    }

    private void OnUpgradeStat_ButtonPressed(StatType statType)
    {
        if (statType != m_statType)
            return;

        if (CanPurchaseUpgrade() == false)
            return;

        if (IsStatMaxxed == true)
            return;

        OnUpgradePurchased?.Invoke(m_statType, GetStatCost());

        UpgradeStat();
    }


    public float GetStatValue()
    {
        return m_playerStat.GetStatValue(m_statCurrentLevel);
    }

    public float GetStatCost()
    {
        return m_playerStat.GetStatCost(m_statCurrentLevel);
    }

    public float GetStatValueNextLevel()
    {
        if(m_statCurrentLevel != m_statMaxLevel)
            return m_playerStat.GetStatValueNextLevel(m_statCurrentLevel);

        return -1f;
    }

    public bool CanPurchaseUpgrade()
    {
        return (Manager_Gold.Instance.CanPurchase(m_playerStat.GetStatCost(m_statCurrentLevel)));
    }


    private void UpgradeStat()
    {
        m_statCurrentLevel++;

        SaveStat();

        OnStatChange?.Invoke();

        OnSendStatInfo?.Invoke(m_statType, m_statCurrentLevel, GetStatCost(), GetStatValue(), GetStatValueNextLevel(), CanPurchaseUpgrade(), IsStatMaxxed);
    }


}
