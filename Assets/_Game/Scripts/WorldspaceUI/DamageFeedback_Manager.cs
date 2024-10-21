using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFeedback_Manager : MonoBehaviour
{
    public static System.Action<GameObject, float> OnDamageFeedbackCreated;

    [SerializeField]
    private GameObject m_damageFeedbackPrefab = null;




    private void OnEnable()
    {
        Projectile.OnProjectileHit += OnProjectileHit;
    }

    private void OnDisable()
    {
        Projectile.OnProjectileHit -= OnProjectileHit;
    }


    private void OnProjectileHit(GameObject target, Vector3 sourcePosition, float damage)
    {
        GameObject instantiatedDamageFeedback = Instantiate(m_damageFeedbackPrefab, target.transform.position, Quaternion.identity);


        OnDamageFeedbackCreated?.Invoke(instantiatedDamageFeedback, damage);
    }


}
