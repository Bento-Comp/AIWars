using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LevelDesignTools : MonoBehaviour
{
    public static System.Action<float> OnGainGold;
    public static System.Action OnLevelUp;

    [Header("Teleport Object Parameters")]
    [SerializeField]
    private GameObject m_objectToTeleport = null;

    [SerializeField]
    private List<Transform> m_targetToTeleportToList = null;

    [Header("Gain Gold Parameters")]
    [SerializeField]
    private float m_goldToGain = 0f;

    public List<Transform> TargetToTeleportToList { get => m_targetToTeleportToList; }

    public void GainGold()
    {
        if (Application.isPlaying == false)
            return;

        if (m_goldToGain < 0)
            return;

        OnGainGold?.Invoke(m_goldToGain);
    }

    public void LevelUp()
    {
        if (Application.isPlaying == false)
            return;

        OnLevelUp?.Invoke();
    }

    public void TeleportObject(Transform target)
    {
        if (Application.isPlaying)
            return;

        if (m_objectToTeleport == null)
            return;

        m_objectToTeleport.transform.position = target.position;
    }

    public void DeleteSaveFile()
    {
        if (Application.isPlaying)
            return;

        PlayerPrefs.DeleteAll();
    }
}
