using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlyphPanelShifter : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;
    private decimal movePos;

    private void Start()
    {
        scrollRect = gameObject.transform.GetComponentInParent<ScrollRect>();
        if (gameObject.transform.childCount > 0)
            movePos = (decimal)5 / (decimal)gameObject.transform.childCount;
        else
            movePos = 0;
    }

    public void LeftClick()
    {
        scrollRect.horizontalNormalizedPosition -= (float)movePos;
    }

    public void RightClick()
    {
        scrollRect.horizontalNormalizedPosition += (float)movePos;
    }
}
