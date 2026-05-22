using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PortSocket : MonoBehaviour
{
    [SerializeField] private PortHead head;

    [SerializeField] private string portName;

    [SerializeField] private float yPos = 3.5f;

    private bool isPortReady;
    public bool isPlugged;

    private void Start()
    {
        head.SelectPortHead += OnSelectPortHead;
    }

    private void OnSelectPortHead()
    {
        isPortReady = true;
    }

    private void OnMouseDown()
    {
        //print("Click Port");

        if (!isPortReady) return;
        
        PlugPortHead();
    }

    private void PlugPortHead()
    {
        //print("Plug Port");

        isPlugged = true;

        isPortReady = false;
        
        ScoreManager.Instance.AddScore(100);

        Motherboard.Instance.AddComponent(portName);

        TutorialManager.Instance.TryFulfillStep($"{portName} Terpasang");

        head.transform.parent = transform;

        head.transform.localPosition = new Vector3(0, yPos, 0);

        head.transform.DOLocalMoveY(10, 2).From().SetRelative();

        GetComponentInChildren<HighlightOnTutorial>().ForceRemove();

        head.GetComponentInChildren<HighlightOnTutorial>().ForceRemove();

        Destroy(head); 
    }
}
