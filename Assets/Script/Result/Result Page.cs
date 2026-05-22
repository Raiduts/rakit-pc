using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultPage : MonoBehaviour
{
    [SerializeField] private Image[] timeStars, scoreStars;
    [SerializeField] private TextMeshProUGUI commentText;

    [SerializeField] private int[] timeReq, scoreReq;
    [SerializeField] private string[] comments;

    [SerializeField] private AudioClip[] audioClips;

    private void Start()
    {
        foreach (Image item in timeStars) item.DOFade(0, 0);

        foreach (Image item in scoreStars) item.DOFade(0, 0);

        commentText.DOFade(0, 0);   

        //ShowStars();
    }

    public void ShowStars()
    {
        int stars = 0;

        Sequence seq = DOTween.Sequence();

        for (int i = 0; i < 3; i++)
        {
            if (Timer.timeLeft > timeReq[i])
            {
                stars++;
                seq.Append(timeStars[i].DOFade(1, 0.5f));
                seq.Join(timeStars[i].transform.DOScale(2, 0.5f).From().SetEase(Ease.InBack));
            }
        }

        for (int i = 0; i < 3; i++)
        {
            if (ScoreManager.Instance.score > scoreReq[i])
            {
                stars++;
                seq.Append(scoreStars[i].DOFade(1, 0.5f));
                seq.Join(scoreStars[i].transform.DOScale(2, 0.5f).From().SetEase(Ease.InBack));
            }
        }
        
        commentText.text = comments[stars / 2];
        AudioManager.Instance.PlayClip(audioClips[stars / 2]);
    
        seq.Append(commentText.DOFade(1, 0.5f));
    }
}
