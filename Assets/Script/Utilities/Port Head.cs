using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortHead : MonoBehaviour
{
    public Action SelectPortHead;

    private void OnMouseDown()
    {
        print("Click Head");
        SelectPortHead?.Invoke();
    }
}
