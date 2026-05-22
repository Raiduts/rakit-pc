using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class StepPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tutorialTitle, tutorialDesc;

    // Start is called before the first frame update

    void Start()
    {
        transform.localScale = Vector3.zero;
        TutorialManager.Instance.OnChangeStep += ChangeTutorialStep;
    }

    public void ChangeTutorialStep(TutorialData tutorialData)
    {
        transform.DOScale(1, 0.5f).SetEase(Ease.OutBack);

        if (tutorialData == null)
        {
            Destroy(gameObject);
            return;
        }

        tutorialTitle.text = tutorialData.tutorialName;
        tutorialDesc.text = tutorialData.tutorialDescription;
    }

    private void OnDestroy()
    {
        TutorialManager.Instance.OnChangeStep -= ChangeTutorialStep;

        transform.DOKill();
    }
}
