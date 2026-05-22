using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AssemblyManager : MonoBehaviour
{
    public static AssemblyManager Instance;

    [Header("Part")]
    [SerializeField] private Transform addonPartPosition;
    [SerializeField] private ComputerPart[] parts;
    private int[] addedParts, spawnedParts;
    [SerializeField] private int[] maxAddedParts, reqAddedParts;
    
    [Header("Button List")]
    private List<AssemblyPartButton> partButtons;
    [SerializeField] private AssemblyPartButton partButtonPref;
    [SerializeField] Transform buttonListContainer;
    [SerializeField] private Button endAssemblyButton;

    [Header("Quest")]
    [SerializeField] private QuestComponent questComponent;

    [Header("Tools")]
    [SerializeField] private bool UnlockAll;
    [SerializeField] private GameObject retryButton;

    public bool isAssembling = false;

    public Action<bool> Assembling;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitializData();

        PrepareAssemblyPartButtons();
        OnUpdateQuest();
    }

    private void InitializData()
    {
        //if (LevelData.Instance == null) return;
        UnlockAll = LevelData.Instance.GetLevelTutorialData().Length == 0;
        retryButton.SetActive(UnlockAll);
        LevelData.Instance.TryShowTutorial();

        parts = LevelData.Instance.GetComputerPart();
        maxAddedParts = LevelData.Instance.GetLimit();
        reqAddedParts = LevelData.Instance.GetReq();

        addedParts = new int[parts.Length];
        spawnedParts = new int[parts.Length];
    }

    private void OnUpdateQuest()
    {
        if (Motherboard.Instance)
        {
            endAssemblyButton.gameObject.SetActive(Motherboard.Instance.HasComponent("Case Lid"));        
        }

        if (UnlockAll)
        {
            return;
        }

        // Add and Check
        for (int i = 0; i < parts.Length; i++)
        {
            if (questComponent)
            {
                addedParts[i] = questComponent.ComponentCount(parts[i].partName);    
            }

            if (i == 0) continue;

            //print($"{addedParts[i]} and {reqAddedParts[i - 1]}");

            if (addedParts[i - 1] >= reqAddedParts[i - 1])
            {
                partButtons[i].UnlockButton();
            }
            else
            {
                partButtons[i].LockButton();
            }
        }
    }

    private void PrepareAssemblyPartButtons()
    {
        partButtons = new List<AssemblyPartButton>();

        foreach (ComputerPart item in parts)
        {
            AssemblyPartButton temp = Instantiate(partButtonPref, buttonListContainer);

            partButtons.Add(temp);

            temp.computerPartPref = item;

            temp.SpawnObject += AddComputerPart;

            temp.PrepareButton();
        }
    }

    public void OnClickAssembly()
    {
        isAssembling = !isAssembling;

        Assembling?.Invoke(isAssembling);
    }

    public void AddPartById(int id)
    {
        if (addedParts[id] >= maxAddedParts[id])
        {
            return;
        }

        AudioManager.Instance.PlaySFX(SFXType.Part_Button);

        addedParts[id]++;

        if (id == 0)
        {
            ComputerPart temp = ObjectSummoner.Instance.SummonObject(parts[id], null);
            TryGetQuestComponent(temp); 
        }
        else
        {
            AddComputerPart(parts[id]);        
        }
    }

    public void AddComputerPart(ComputerPart part)
    {
        if (!CheckAvailibility(part.partName))
        {
            return;
        }

        AudioManager.Instance.PlaySFX(SFXType.Part_Button);

        ComputerPart temp = ObjectSummoner.Instance.SummonObject(part, addonPartPosition);

        UpdateAvailability(temp.partName);

        TryGetQuestComponent(temp);
    }

    private void UpdateAvailability(string name)
    {
        for (int i = 0; i < parts.Length; i++)
        {
            if (name.Equals(parts[i].partName))
            {
                spawnedParts[i]++;
            }
        }
    }

    public bool CheckAvailibility(string name)
    {
        for (int i = 0; i < parts.Length; i++)
        {
            //print(i);

            if (name.Equals(parts[i].partName))
            {
                //print("omag");
                return spawnedParts[i] < maxAddedParts[i];
            }
        }

        return true;
    }

    private void TryGetQuestComponent(ComputerPart part)
    {
        QuestComponent temp = part.GetComponentInChildren<QuestComponent>();

        if (temp)
        {
            questComponent = temp;
            questComponent.UpdateQuest += OnUpdateQuest;
            //print("quest component " + questComponent.GetInstanceID());
            part.transform.position = Vector3.zero;
        }
    }

    public void BackToDashboard()
    {
        LoadingScreen.Instance.CloseScreen(() =>
        {
            Destroy(LevelData.Instance.gameObject);
        
            SceneManager.LoadScene("Dashboard");
        });
    }

    public void Retry()
    {
        SceneManager.LoadScene("Assembly Scene");
    }
}
