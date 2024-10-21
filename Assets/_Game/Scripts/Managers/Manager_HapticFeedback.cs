using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_HapticFeedback : MonoBehaviour
{



    private void OnEnable()
    {
        Taptic.tapticOn = true;

        PlayerHealth.OnPlayerTakeDamage += OnPlayerTakeDamage;
        PlayerShoot.OnPlayerShoot += OnPlayerShoot;
        Manager_Gold.OnGainGold += OnGainGold;
        PlayerGearBag.OnGainGear += OnGainGear;
    }

    private void OnDisable()
    {
        Taptic.tapticOn = false;

        PlayerHealth.OnPlayerTakeDamage -= OnPlayerTakeDamage;
        PlayerShoot.OnPlayerShoot -= OnPlayerShoot;
        Manager_Gold.OnGainGold -= OnGainGold;
        PlayerGearBag.OnGainGear -= OnGainGear;
    }


    private void OnGainGold(float amount)
    {
        Taptic.Light();
    }

    private void OnGainGear(float amount)
    {
        Taptic.Light();
    }

    private void OnPlayerShoot()
    {
        Taptic.Light();
    }

    private void OnPlayerTakeDamage()
    {
        Taptic.Heavy();
    }

}
