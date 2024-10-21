using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsController : MonoBehaviour
{
    public static System.Action<bool> OnPurchasableUpgradeAvailable;

    [SerializeField]
    private List<PlayerStat> m_playerStatList = null;


    private void OnEnable()
    {
        Manager_Gold.OnBroadcastGoldHeld += CheckPurchasableUpgradeState;
    }

    private void OnDisable()
    {
        Manager_Gold.OnBroadcastGoldHeld -= CheckPurchasableUpgradeState;
    }

    private void CheckPurchasableUpgradeState(float goldHeld)
    {
        bool isAnyUpgradePurchasable = IsAnyUpgradePurchasable(goldHeld);

        OnPurchasableUpgradeAvailable?.Invoke(isAnyUpgradePurchasable);
    }


    private bool IsAnyUpgradePurchasable(float goldHeld)
    {
        for (int i = 0; i < m_playerStatList.Count; i++)
        {
            if (m_playerStatList[i].GetStatCost() <= goldHeld)
                return true;
        }

        return false;
    }
}
