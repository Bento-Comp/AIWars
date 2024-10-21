using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XpGiver : MonoBehaviour
{
    public static System.Action<float> OnGiveXp;

    [SerializeField]
    private float m_xpValue = 1f;



    public void GiveXp()
    {
        OnGiveXp?.Invoke(m_xpValue);
    }

}
