using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ThermalPaste : MonoBehaviour
{
    [SerializeField] private Transform thermalSyringe, paste;

    private bool canApplyPaste = true;

    private void OnMouseDown()
    {
        if (!canApplyPaste || Motherboard.Instance.isMotherboadLocked) return;

        canApplyPaste = false;

        print("Apply Thermal Paste");

        ScoreManager.Instance.AddScore(75);

        AnimatePasting();
    }

    private void AnimatePasting()
    {
        Sequence seq = DOTween.Sequence();

        seq.AppendCallback(() =>
        {
            thermalSyringe.gameObject.SetActive(true);
            paste.gameObject.SetActive(true);
        });

        seq.Append(thermalSyringe.DOMoveZ(-0.8f, 1).From().SetRelative());
        seq.Append(paste.DOScale(0, 2).From());
        seq.AppendCallback(() =>
        {
            Motherboard.Instance.AddComponent("Thermal Paste");

            TutorialManager.Instance.TryFulfillStep($"Thermal Paste Terpasang");
            //Motherboard.Instance.UpdateQuest?.Invoke();

            Destroy(thermalSyringe.gameObject);
        });
    }
}
