using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewCounter : MonoBehaviour
{
    [SerializeField] private string screwSetName;

    [SerializeField] private List<Screw> screws = new List<Screw>();

    private void Start()
    {
        //Motherboard.Instance.UpdateQuest += OnUpdateQuest;    

        foreach (Screw screw in screws)
        {
            screw.ScrewPlaced += OnUpdateQuest;
        }
    }

    private void OnUpdateQuest()
    {
        //print("Cek Baut!");

        for (int i = screws.Count - 1; i >= 0; i--)
        {
            //print($"Cek Baut {screws[i]}");

            if (screws[i].IsPlaced())
            {
                //print("Baut Terpasang");

                screws.RemoveAt(i);
            }
        }

        if (screws.Count <= 0)
        {
            TutorialManager.Instance.TryFulfillStep($"{screwSetName} Terpasang");
        }
    }

    private void OnDestroy()
    {
        foreach (Screw screw in screws)
        {
            screw.ScrewPlaced -= OnUpdateQuest;
        }
    }
}
