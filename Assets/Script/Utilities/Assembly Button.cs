using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AssemblyButton : MonoBehaviour
{
    private Button button;
    private RectTransform rectTransform;
    private Image imageButton;

    [SerializeField] private TextMeshProUGUI textButton;
    [SerializeField] private Sprite assemblingColor, inspectColor;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        rectTransform = GetComponent<RectTransform>();
        imageButton = GetComponent<Image>();

        AssemblyManager.Instance.Assembling += OnAssembly;   
    }

    private void OnAssembly(bool isAssembling)
    {
        if (isAssembling)
        {
            rectTransform.sizeDelta = new Vector2(382, 128);
            //rectTransform.DOAnchorPosX((320 - 192) / 2, 0.25f);
            textButton.text = "Keluar dari Mode Rakit";
            imageButton.sprite = inspectColor;
        }
        else
        {
            rectTransform.sizeDelta = new Vector2(256, 128);
            //rectTransform.DOAnchorPosX(0, 0.25f);
            textButton.text = "Mulai Rakit";
            imageButton.sprite = assemblingColor;
        }

        //ShowButton();
    }

    private void ShowButton()
    {
        transform.DOScale(1, 0.5f).From(0).SetEase(Ease.OutBack);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
