using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class StatSlot_UI : MonoBehaviour
{
    public static System.Action<StatType> OnAskStatInfo;
    public static System.Action<StatType> OnUpgradeStat_ButtonPressed;

    [SerializeField]
    private StatType m_statType;

    [SerializeField]
    private TMP_Text m_statLevelText = null;

    [SerializeField]
    private TMP_Text m_upgradeCostText = null;

    [SerializeField]
    private TMP_Text m_statValueText = null;

    [SerializeField]
    private GameObject m_upgradeButtonCurrencyIcon = null;

    [SerializeField]
    private string m_statValuePrefix = "";

    [SerializeField]
    private Color m_upgradeCostTextPurchasableColor = Color.white;

    [SerializeField]
    private Color m_upgradeCostTextNotPurchasableColor = Color.red;


    private void OnEnable()
    {
        PlayerStat.OnSendStatInfo += UpdateStatSlotInfo;
        PlayerGearBag.OnBroadcastGearPosessed += OnBroadcastGearPosessed;

        OnAskStatInfo?.Invoke(m_statType);
    }

    private void OnDisable()
    {
        PlayerStat.OnSendStatInfo -= UpdateStatSlotInfo;
        PlayerGearBag.OnBroadcastGearPosessed -= OnBroadcastGearPosessed;
    }


    private void OnBroadcastGearPosessed(float gearPosessed, float bagSize)
    {
        OnAskStatInfo?.Invoke(m_statType);
    }

    private void UpdateStatSlotInfo(StatType statType, int level, float cost, float statValue, float nextLevelStatValue, bool canBePurchased, bool isStatMaxxed)
    {
        if (statType != m_statType)
            return;

        if (isStatMaxxed)
            m_statValueText.text = "Stat is maxxed! Value : " + statValue.ToString("F2");
        else
            m_statValueText.text = m_statValuePrefix + statValue.ToString("F2") + " to " + "<color=#5DFF72>" + nextLevelStatValue.ToString("F2");


        if (isStatMaxxed == false)
        {
            m_upgradeButtonCurrencyIcon.SetActive(true);

            m_statLevelText.text = "Level " + level.ToString();


            if (cost < 10)
                m_upgradeCostText.text = cost.ToString("F2");
            else if (cost < 100)
                m_upgradeCostText.text = cost.ToString("F1");
            else
                m_upgradeCostText.text = cost.ToString("F0");

            UpdatePurchasableState(canBePurchased);
        }
        else
        {
            m_upgradeButtonCurrencyIcon.SetActive(false);

            m_statLevelText.text = "Level max";
            m_upgradeCostText.text = "Max";
            m_upgradeCostText.color = m_upgradeCostTextPurchasableColor;

        }
    }

    private void UpdatePurchasableState(bool canBePurchased)
    {
        if (canBePurchased)
            m_upgradeCostText.color = m_upgradeCostTextPurchasableColor;
        else
            m_upgradeCostText.color = m_upgradeCostTextNotPurchasableColor;
    }


    //called by button
    public void UpgradeStat_ButtonPressed()
    {
        OnUpgradeStat_ButtonPressed?.Invoke(m_statType);
    }
}
