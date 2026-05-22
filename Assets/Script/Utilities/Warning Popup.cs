using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class WarningPopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private AudioClip warningClip;

    private void Start()
    {
        Show();
    }

    public void SetText(string text)
    {
        warningText.text = text;
    }

    private void Show()
    {
        AudioManager.Instance.PlayClip(warningClip);

        RectTransform rect = GetComponent<RectTransform>();

        Sequence seq = DOTween.Sequence();  

        seq.Append(rect.DOAnchorPosY(45, 0.25f).SetEase(Ease.OutBack).From());
        seq.AppendInterval(1f);
        seq.Append(rect.DOAnchorPosY(45, 0.25f).SetEase(Ease.InBack));

        seq.AppendCallback(() =>
        {
            WarningPopper.Instance.isShowing = false;
            Destroy(gameObject);
        });
    }
}
