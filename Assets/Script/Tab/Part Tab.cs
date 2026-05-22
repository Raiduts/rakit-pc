using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PartTab : MonoBehaviour
{
    [SerializeField] private Image partImage;
    [SerializeField] private TextMeshProUGUI partNameText;
    [SerializeField] private Button detailButton;
    private ShowcaseData showcaseData;

    [SerializeField] private PartDetailPanel partDetailPanelPref;

    private void Start()
    {
        detailButton.onClick.AddListener(DetailButtonOnClick);
    }

    public void SetPartTab(ShowcaseData part)
    {
        partImage.sprite = part.partSprite;
        partNameText.text = part.partName;
        showcaseData = part;
    }

    public void DetailButtonOnClick()
    {
        PartDetailPanel temp = Instantiate(partDetailPanelPref, ResourceManager.Instance.mainCanvas.transform);

        temp.PreparePanel(showcaseData);        
    }
}
