using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearSpawner : MonoBehaviour
{
    //GameObject : gear currency reference; Vector3 : originPosition; Vector3 : spawn destination movement in parabola; float : spawn height parabola; float : gearValue
    public static System.Action<GameObject, Vector3, Vector3, float, float> OnSpawnGear;
    public System.Action<float> OnSendGearValue;

    [SerializeField]
    private GameObject m_gearCollectablePrefab = null;

    [SerializeField]
    private float m_gearValueToSpawn = 3;

    [SerializeField]
    private int m_maxGearToSpawn = 7;

    [SerializeField]
    private float m_minRadiusOfSpawn = 0.5f;

    [SerializeField]
    private float m_maxRadiusOfSpawn = 2f;

    [SerializeField]
    private float m_spawnHeightParabola = 2f;

    private void Start()
    {
        OnSendGearValue?.Invoke(m_gearValueToSpawn);
    }


    public void SpawnGear(Vector3 spawnOriginPosition)
    {
        int gearCountToSpawn = m_maxGearToSpawn;

        if (m_gearValueToSpawn < m_maxGearToSpawn)
            gearCountToSpawn = (int)m_gearValueToSpawn;

        float valuePerGear = m_gearValueToSpawn / gearCountToSpawn;


        for (int i = 0; i < gearCountToSpawn; i++)
        {
            Vector3 gearSpawnDestination = CalculateGearSpawnPosition(spawnOriginPosition);

            GameObject instantiatedGearCurrency = Instantiate(m_gearCollectablePrefab, spawnOriginPosition, Quaternion.identity);

            OnSpawnGear?.Invoke(instantiatedGearCurrency, spawnOriginPosition, gearSpawnDestination, m_spawnHeightParabola, valuePerGear);
        }
    }

    private Vector3 CalculateGearSpawnPosition(Vector3 spawnOriginPosition)
    {
        Vector3 gearSpawnDestination = Random.insideUnitSphere;

        gearSpawnDestination.y = 0f;

        gearSpawnDestination.Normalize();

        gearSpawnDestination *= Random.Range(m_minRadiusOfSpawn, m_maxRadiusOfSpawn);

        gearSpawnDestination += spawnOriginPosition;

        return gearSpawnDestination;
    }
}
