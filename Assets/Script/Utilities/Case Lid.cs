using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CaseLid : MonoBehaviour
{
    private bool isClosed;

    private void OnMouseDown()
    {
        if (TutorialManager.Instance.GetCurrentTutorialStep() != null)
        {
            if (!TutorialManager.Instance.GetCurrentTutorialStep().tutorialName.Equals("Tutup Casing"))
            {
                return;
            }
        }

        isClosed = !isClosed;

        transform.DOLocalMoveY(isClosed? 0 : 1 , 1);    

        if (Motherboard.Instance)
        {
            if (isClosed)
            {
                Motherboard.Instance.AddComponent("Case Lid");        
            }
            else
            {
                Motherboard.Instance.RemoveComponent("Case Lid");
            }
        }

        TutorialManager.Instance.TryFulfillStep("Case Lid Terpasang");
    }
}
