using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Cinemachine.DocumentationSortingAttribute;

public class DashboardManager : MonoBehaviour
{
    public static DashboardManager Instance;

    [SerializeField] private ComponentManager componentManager;

    [SerializeField] private Transform computer3D;

    [SerializeField] private LevelData[] levelTutorial;
    [SerializeField] private LevelData levelMandiri;

    private void Awake()
    {
        Application.targetFrameRate = -1;
        QualitySettings.vSyncCount = 0;
    }

    private void Start()
    {
        //ObjectSummoner.Instance.SummonObject(computer3D);
    }

    public void OpenComponentTab()
    {
        ComponentManager temp = Instantiate(componentManager, ResourceManager.Instance.mainCanvas.transform);

        temp.gameObject.SetActive(true);

        temp.transform.DOScale(1, 0.5f).From(0).SetEase(Ease.OutQuad);
    }

    public void GoToTutorial(int level)
    {
        Instantiate(levelTutorial[level]);
        ChangeScene("Assembly Scene");
    }

    public void GoToMandiri()
    {
        Instantiate(levelMandiri);
        ChangeScene("Assembly Scene");
    }

    public void ChangeScene(string newScene)
    {
        CinemachineController.Instance.ChangeTarget(computer3D, () =>
        {
            LoadingScreen.Instance.CloseScreen(() =>
            {
                SceneManager.LoadScene(newScene);
            });
        });

    }
}
