using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnHoverSelectAssembly : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private RectTransform backgroundRect;
    [SerializeField] private Image backgroundImage;
    private float startW, startH;
    [SerializeField] private float endW, endH;

    private void Start()
    {
        //backgroundRect = GetComponent<RectTransform>();

        startW = backgroundRect.rect.width;
        startH = backgroundRect.rect.height;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        backgroundRect.DOSizeDelta(new Vector2(endW, endH), 0.25f);
        backgroundImage.DOFade(0.9f, 0.25f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        backgroundRect.DOSizeDelta(new Vector2(startW, startH), 0.25f);
        backgroundImage.DOFade(1, 0.25f);
    }
}
