using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notification_UpgradeButton_UI : MonoBehaviour
{
    [SerializeField]
    private GameObject m_notificationIcon = null;



    private void OnEnable()
    {
        PlayerStatsController.OnPurchasableUpgradeAvailable += OnPurchasableUpgradeAvailable;
    }

    private void OnDisable()
    {
        PlayerStatsController.OnPurchasableUpgradeAvailable -= OnPurchasableUpgradeAvailable;
    }

    private void OnPurchasableUpgradeAvailable(bool isAnyUpgradePurchasable)
    {
        m_notificationIcon.SetActive(isAnyUpgradePurchasable);
    }

}
