using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaOfDetection : MonoBehaviour
{
    [SerializeField]
    private Transform m_scaleController = null;

    [SerializeField]
    private SpriteRenderer m_areaSprite = null;


    public void UpdateAreaSpriteColor(Color newColor)
    {
        if (m_areaSprite.color == newColor)
            return;

        m_areaSprite.color = newColor;
    }

    public void UpdateScale(Vector3 newScale)
    {
        m_scaleController.localScale = newScale;
    }

    public void UpdateScale(float newScale)
    {
        m_scaleController.localScale = Vector3.one * newScale;
    }
}
