using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour
{
    public static LevelData Instance;

    [SerializeField] private TutorialData[] levelTutorialDatas;
    [SerializeField] private ComputerPart[] levelParts;
    [SerializeField] private int[] addLimit, addReq;

    [SerializeField] private bool skipInstance;
    public string levelUnlocked;

    [Header("Optional")]
    [SerializeField] private TutorialPage tutorialPage;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        } 
           
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void OnFinishLevel()
    {
        PlayerPrefs.SetInt(levelUnlocked, 1);
    }

    public void TryShowTutorial()
    {
        if (tutorialPage == null) return;

        Instantiate(tutorialPage, transform.GetChild(0));
    }

    public TutorialData[] GetLevelTutorialData()
    {
        return levelTutorialDatas;
    }

    public ComputerPart[] GetComputerPart()
    {
        return levelParts;
    }

    public int[] GetLimit()
    {
        return addLimit;
    }

    public int[] GetReq()
    {
        return addReq;
    }
}
