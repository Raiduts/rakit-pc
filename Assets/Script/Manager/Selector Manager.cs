using System;
using TMPro;
using UnityEngine;

public class SelectorManager : MonoBehaviour
{
    public static SelectorManager Instance;

    [SerializeField] private TextMeshProUGUI titleText;

    public Action<int> ChangeObject;

    private void Awake()
    {
        Instance = this;
    }

    public void OnClickNext()
    {
        ChangeObject?.Invoke(1);
    }

    public void OnClickPrev()
    {
        ChangeObject?.Invoke(-1);
    }

    public void ChangeTitleText(string title)
    {
        titleText.text = title; 
    }
}
