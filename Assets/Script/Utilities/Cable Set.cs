using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableSet : MonoBehaviour
{
    [SerializeField] private string CableSetName;

    [SerializeField] PortSocket[] sockets;

    [SerializeField] GameObject[] cableSetObjects;

    private void Start()
    {
        TutorialManager.Instance.OnChangeStep += ListenToTutorial;

        if (TutorialManager.Instance.tutorialDatas.Length == 0)
        {
            ShowSetObjects(true);
        }
        else
        {
            ListenToTutorial(TutorialManager.Instance.GetCurrentTutorialStep());
        }

        if (Motherboard.Instance)
        {
            Motherboard.Instance.UpdateQuest += OnUpdateQuest;        
        }
    }

    private void ListenToTutorial(TutorialData data)
    {
        if (data == null || data.tutorialReq != $"{CableSetName} Terpasang")
        {
            return;
        }

        ShowSetObjects(true);

    }

    private void ShowSetObjects(bool isActive)
    {
        foreach (GameObject item in cableSetObjects)
        {
            item.SetActive(isActive);
        }
    }

    private void OnUpdateQuest()
    {
        foreach (PortSocket item in sockets)
        {
            if (!item.isPlugged)
            {
                return;
            }
        }

        Motherboard.Instance.UpdateQuest -= OnUpdateQuest;

        Motherboard.Instance.AddComponent($"{CableSetName}");

        if (TutorialManager.Instance)
        {
            TutorialManager.Instance.TryFulfillStep($"{CableSetName} Terpasang");
        }
    }
}
