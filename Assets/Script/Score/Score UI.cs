using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [Header("Problem")]
    [SerializeField] private List<Problem> partProblem;

    [Header("Ref")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI problemText;
    [SerializeField] private Image diagnoseBackgroundImage;
    [SerializeField] private Button diagnoseButton;
    [SerializeField] private Image errorIcon;

    private int score;

    private void Start()
    {
        ScoreManager.ChangeScore += OnChangeScore;

        diagnoseButton.onClick.AddListener(StartDiagnose);

        OnChangeScore(0);
    }

    private void OnChangeScore(int score)
    {
        this.score = score;

        //if (Motherboard.Instance)
        //{
        //    CheckProblem();        
        //}
    }

    public void StartDiagnose()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(diagnoseButton.transform.DOScale(0, 0.25f).SetEase(Ease.InBack));
        seq.Append(diagnoseBackgroundImage.DOFade(0, 0.25f));

        seq.AppendCallback(() =>
        {
            Destroy(diagnoseBackgroundImage.gameObject);
            StartCoroutine(IEDiagnose());
        });
    }

    private IEnumerator IEDiagnose()
    {
        int index = 0;

        foreach (Problem problem in partProblem)
        {
            problem.printedProblem = "";
            float req = 0, reqCleared = 0;

            foreach (ProblemDetail detail in problem.problems)
            {
                req++;

                if (!Motherboard.Instance.HasComponent(detail.req))
                {
                    problem.printedProblem += $"{detail.reason}\n";
                }
                else
                {
                    reqCleared++;
                }
            }

            int percentage = (int)((reqCleared / req) * 100);

            for (int i = 0; i <= percentage; i++)
            {
                problem.percentage.text = $"{(i == 100 ? "<color=#53D958>" : "<color=#D95353>")}{i}%";
                yield return new WaitForSeconds(Time.deltaTime * 5);
            }

            if (percentage == 100)
            {
                problem.printedProblem = $"Tidak Ada Masalah pada {problem.partName}, HEBAT!";
            }

            PrintProblem(index);
    
            yield return new WaitForSeconds(1);

            index++;
        }


        for (int i = 0; i <= score; i++)
        {
            scoreText.text = $"{i}";
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    public void PrintProblem(int id)
    {
        Problem problem = partProblem[id];

        if (problem.printedProblem.Equals($"Tidak Ada Masalah pada {problem.partName}, HEBAT!"))
        {
            errorIcon.DOFade(0, 0.25f);
        }
        else
        {
            errorIcon.DOFade(1, 0.25f);
        }

        problemText.text = problem.printedProblem;
    }

    private void OnDestroy()
    {
        ScoreManager.ChangeScore -= OnChangeScore;
    }
}

[System.Serializable]
public class Problem
{
    public string partName;
    public List<ProblemDetail> problems;
    public TextMeshProUGUI percentage;

    [HideInInspector]
    public string printedProblem;
}

[System.Serializable]
public class ProblemDetail
{
    public string req;
    [TextArea] public string reason;
}
