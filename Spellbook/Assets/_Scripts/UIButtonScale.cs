using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonScale : MonoBehaviour
{
    public void ScaleUp()
    {
        transform.localScale = new Vector3(1.2f, 1.2f, 0);
    }

    public void ScaleDown()
    {
        transform.localScale = new Vector3(1f, 1f, 0);
    }
}
