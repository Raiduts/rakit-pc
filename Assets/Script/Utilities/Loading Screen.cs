using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen Instance;

    [SerializeField] private VerticalLayoutGroup verticalLayoutGroup;
    private RectTransform[] loadingImages;

    [Header("Title Purpose")]
    [SerializeField] private Transform cameraTarget;
    [SerializeField] private TextMeshProUGUI resText;
    [SerializeField] private Button titleBt;
    [SerializeField] private TutorialPage dashboardTutorial;

    private float deltaTime;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        cameraTarget.position = cameraTarget.position + new Vector3(0, 20, 0);

        titleBt.interactable = true;

        titleBt.onClick.AddListener(ResetCamera);
        //Invoke(nameof(ResetCamera), 3);

        loadingImages = verticalLayoutGroup.GetComponentsInChildren<RectTransform>();

        SceneManager.sceneLoaded += OnLoadScene;

        OpenScreen();
    }

    private void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;

        resText.text = 
            $"res : {Screen.width} x {Screen.height}\n" +
            $"scaled : {Screen.width * 0.5} x {Screen.height * 0.5}\n" +
            $"fps : {string.Format("{0:0.} FPS", fps)}";
    }

    public void ResetCamera()
    {
        titleBt.interactable = false;

        ObjectSummoner.Instance.CallCamera();

        // Show Tutorial
        Invoke(nameof(ShowTutorial), 1.5f);
    }

    private void ShowTutorial()
    {
        Instantiate(dashboardTutorial, transform.GetChild(0));
    }

    private void OnLoadScene(Scene arg0, LoadSceneMode arg1)
    {
        print("Loaded");
        OpenScreen();
    }

    private void ResetScreen(float value)
    {
        for (int i = 1; i < loadingImages.Length; i++)
        {
            loadingImages[i].localScale = new(value, 1);
        }
    }

    public void CloseScreen(Action action)
    {
        ResetScreen(0);

        verticalLayoutGroup.childAlignment = TextAnchor.MiddleRight;

        Sequence seq = DOTween.Sequence();

        for (int i = 1; i < loadingImages.Length; i++)
        {
            loadingImages[i].pivot = new(0, 0.5f);

            seq.Append(loadingImages[i].DOScaleX(1, 0.25f));
        }

        seq.AppendCallback(() =>
        {
            action?.Invoke();
        });
    }

    public void OpenScreen()
    {
        ResetScreen(1);

        verticalLayoutGroup.childAlignment = TextAnchor.MiddleLeft;

        Sequence seq = DOTween.Sequence();

        for (int i = 1; i < loadingImages.Length; i++)
        {
            loadingImages[i].pivot = new(1, 0.5f);

            seq.Append(loadingImages[i].DOScaleX(0, 0.25f));
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnLoadScene;
    }
}
