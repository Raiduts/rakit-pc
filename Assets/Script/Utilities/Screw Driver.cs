using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ScrewDriver : MonoBehaviour
{
    public Action<ScrewDriver> DriverOnPlace;

    // Start is called before the first frame update
    void Start()
    {
        MoveToScrew();
    }

    private void MoveToScrew()
    {
        transform.DOLocalMoveZ(50, 1)
            .From()
            .OnComplete(() =>
        {   
            DriverOnPlace?.Invoke(this);
        });
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
