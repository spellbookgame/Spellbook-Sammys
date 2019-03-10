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
        Debug.Log("child count: " + gameObject.transform.childCount);
        movePos = (decimal)5 / (decimal)gameObject.transform.childCount;
        Debug.Log(movePos);
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
