using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PartDetailPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText, overviewText, factsText, functionsText, variationText;

    // Start is called before the first frame update
    void Start()
    {
        OpenPanel();
    }

    private void OpenPanel()
    {
        transform.DOScale(1, 0.5f).From(0).SetEase(Ease.OutBack);
    }

    public void ClosePanel()
    {
        transform.DOScale(0, 0.25f).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }

    public void PreparePanel(ShowcaseData data)
    {
        if (data.part3D != null)
        {
            ObjectSummoner.Instance.SummonObject(data.part3D);        
        }

        titleText.text = data.partName;

        variationText.text = data.partSeries;

        overviewText.text = data.overview;

        foreach (string fact in data.facts)
        {
            factsText.text += $"- {fact}\n";
        }

        foreach (string function in data.functions)
        {
            functionsText.text += $"- {function}\n";
        }
    }
}
