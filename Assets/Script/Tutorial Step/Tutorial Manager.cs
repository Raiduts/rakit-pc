using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;

    public TutorialData[] tutorialDatas;

    [SerializeField] private List<string> reqFinished = new();

    [SerializeField] private GameObject clearStage;

    public int currentStepIndex;

    public Action<TutorialData> OnChangeStep;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Invoke(nameof(InitStep), 0.1f);
    }

    private void InitStep()
    {
        GetTutorialData();

        if (tutorialDatas.Length > 0)
        {
            AnnounceNewStep();        
        }
        else
        {
            OnChangeStep?.Invoke(null);
        }
    }

    public TutorialData GetCurrentTutorialStep()
    {
        if (tutorialDatas.Length == 0)
        {
            return null;
        }

        return tutorialDatas[currentStepIndex];
    }

    private void GetTutorialData()
    {
        tutorialDatas = LevelData.Instance.GetLevelTutorialData();
    }

    public void TryFulfillStep(string requrement = "null")
    {
        if (currentStepIndex >= tutorialDatas.Length)
        {
            return;
        }

        if (!reqFinished.Contains(requrement))
        {        
            reqFinished.Add(requrement);
        }

        if (requrement == tutorialDatas[currentStepIndex].tutorialReq || reqFinished.Contains(tutorialDatas[currentStepIndex].tutorialReq))
        {
            currentStepIndex++;

            AnnounceNewStep();
        }
    }

    private void AnnounceNewStep()
    {
        if (currentStepIndex >= tutorialDatas.Length - 1)
        {
            LevelData.Instance.OnFinishLevel();
            clearStage.SetActive(true);
            OnChangeStep?.Invoke(null);
            return;
        }

        OnChangeStep?.Invoke(tutorialDatas[currentStepIndex]);

        TryFulfillStep();
    }
}

[System.Serializable]
public class TutorialData
{
    public string tutorialName;
    public string tutorialReq;
    
    [TextArea] public string tutorialDescription;
}
