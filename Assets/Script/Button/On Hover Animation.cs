using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnHoverAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GetComponent<Button>().interactable)
        {
            transform.DOScale(1.05f, 0.25f).SetEase(Ease.OutBack);        
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (GetComponent<Button>().interactable)
        {
            transform.DOScale(1, 0.25f).SetEase(Ease.InBack);
        }
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
