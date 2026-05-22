using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static float timeLeft;
    private bool isTicking;

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Button toComputerBt;

    private void Start()
    {
        //print(TutorialManager.Instance.tutorialDatas.Length);

        TutorialManager.Instance.OnChangeStep += OnChangeStep;
    }

    private void OnChangeStep(TutorialData data)
    {
        if (TutorialManager.Instance.tutorialDatas.Length > 0)
        {
            TutorialManager.Instance.OnChangeStep -= OnChangeStep;
            gameObject.SetActive(false);
            return;
        }
        else
        {
            isTicking = true;
            timeLeft = 300;
        }
    }

    private void Update()
    {
        if (!isTicking) return;

        timeLeft -= Time.deltaTime;

        if (timeLeft < 1)
        {
            isTicking = false;
            StartCoroutine(OnTimesUp());
        }

        UpdateTimeLeft();
    }

    private IEnumerator OnTimesUp()
    {
        WarningPopper.Instance.ShowWarning("WAKTU HABIS");

        yield return new WaitForSeconds(1f);

        toComputerBt.onClick?.Invoke();
    }

    private void UpdateTimeLeft()
    {
        int minutes = Mathf.FloorToInt(timeLeft / 60);
        int seconds = Mathf.FloorToInt(timeLeft % 60);

        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    private void OnDestroy()
    {
        TutorialManager.Instance.OnChangeStep -= OnChangeStep;
    }
}
