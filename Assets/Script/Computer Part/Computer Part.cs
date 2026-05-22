using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class ComputerPart : MonoBehaviour
{
    [Header("Part Data")]
    public string partName;
    public Sprite partImageSprite;
    [TextArea]
    public string description;

    public float multiplyer = 1;
    public Vector3 spawnOffset = Vector3.zero;

    private void Start()
    {
        if (SelectorManager.Instance)
        {
            SelectorManager.Instance.ChangeTitleText(partName);
        }
    }

    public void PopAnimate()
    {
        transform.DOScale(0, 0.5f).From().SetEase(Ease.OutQuad);
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
