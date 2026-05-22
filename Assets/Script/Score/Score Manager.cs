using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int score;

    public static Action<int> ChangeScore;

    private void Awake()
    {
        Instance = this;
    }

    public void AddScore(int scoreAdder)
    {
        score += scoreAdder;

        print($"Score : {score}");

        ChangeScore?.Invoke(score);
    }
}
