using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPage : MonoBehaviour
{
    [Header("Tutorial Ref")]
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descText;
    [SerializeField] private TextMeshProUGUI indexText;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Image tutorialImage;
    [SerializeField] private Button prevBt, nextBt;
    [SerializeField] private Transform tutorialPanel;

    [Header("Tutorial")]
    [SerializeField] private TutorialUXData[] tutorialsData;

    private int index;

    private void Start()
    {
        index = 0;

        ChangeTutorial();

        ShowTutorial();
    }

    private void ShowTutorial()
    {
        if (ComputerMonitor.Instance)
        {
            ComputerMonitor.Instance.monitorCollider.enabled = false;
        }

        Sequence seq = DOTween.Sequence();

        seq.Append(backgroundImage.DOFade(0, 0.5f).From());
        seq.Append(tutorialPanel.DOScale(0, 0.5f).From().SetEase(Ease.OutBack));
    }

    public void HideTutorial()
    {
        if (ComputerMonitor.Instance)
        {
            ComputerMonitor.Instance.monitorCollider.enabled = true;
        }

        Sequence seq = DOTween.Sequence();

        seq.Append(tutorialPanel.DOScale(0, 0.5f).SetEase(Ease.InBack));
        seq.Append(backgroundImage.DOFade(0, 0.5f));

        seq.AppendCallback(() =>
        {
            Destroy(gameObject);
        });
    }

    private void ChangeTutorial()
    {
        TutorialUXData tutorialData = tutorialsData[index];

        titleText.text = tutorialData.title;
        descText.text = tutorialData.desc;
        tutorialImage.sprite = tutorialData.sprite;

        indexText.text = $"{index + 1}/{tutorialsData.Length}";
        prevBt.interactable = index > 0;
        nextBt.interactable = index < tutorialsData.Length - 1;
    }

    public void ChangeIndex(int adder)
    {
        index = Mathf.Clamp(index + adder, 0, tutorialsData.Length - 1);

        ChangeTutorial();
    }
}

[System.Serializable]   
public class TutorialUXData
{
    public string title; 
    public string desc;
    public Sprite sprite;
}
