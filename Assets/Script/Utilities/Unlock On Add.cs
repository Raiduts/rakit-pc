using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockOnAdd : MonoBehaviour
{
    [SerializeField] private GameObject[] objects;
    [SerializeField] private string reqPart;

    private void Start()
    {
        Motherboard.Instance.UpdateQuest += OnUpdateQuest;        
    }

    private void OnUpdateQuest()
    {
        if (Motherboard.Instance.HasComponent(reqPart))
        {
            foreach (GameObject item in objects)
            {
                item.SetActive(true);   
            }
        }
    }
}
