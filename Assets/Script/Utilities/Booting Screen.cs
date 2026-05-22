using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BootingScreen : MonoBehaviour
{
    [SerializeField] private Slider loadingSlider;
    [SerializeField] private TextMeshProUGUI welcomeText;
    [SerializeField] private GameObject loadingPage;

    private void Start()
    {
        TutorialManager.Instance.OnChangeStep += OnChangeStep;
    }

    public void StartBooting()
    {
        StartCoroutine(LoadingBar());
    }

    private void OnChangeStep(TutorialData data)
    {
        if (data == null) { 
            Destroy(gameObject);    
            TutorialManager.Instance.OnChangeStep -= OnChangeStep;
            return;
        }

        if (data.tutorialName.Equals("Selesaikan Booting"))
        {
            TutorialManager.Instance.OnChangeStep -= OnChangeStep;
            StartBooting();
        }
    }

    private IEnumerator LoadingBar()
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i <= 100; i++)
        {
            loadingSlider.value = i / 100f;

            yield return new WaitForSeconds(Time.deltaTime * 15);
        }

        yield return new WaitForSeconds(1f);

        loadingPage.SetActive(false);

        welcomeText.DOFade(1, 0.5f);

        yield return new WaitForSeconds(0.5f);

        TutorialManager.Instance.TryFulfillStep("Booting Finished");
    }
}
