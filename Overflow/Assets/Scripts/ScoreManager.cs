using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    #region Singleton

    public static ScoreManager Instance { get; private set; }

    #endregion

    #region Members

    [SerializeField] private int baseScore = 3000; // Needs base score to not instantly lose
    private int score = 0; // Needs base score to not instantly lose

    [SerializeField] private int scoreIncrementValue = 100;
    [SerializeField] private int scoreDecrementValue = 10;

    public static event Action onLose;

    #endregion

    #region Monobehaviours

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ResetScore();

        UIManager.Instance.UpdateScoreText(score);
    }

    #endregion

    #region Public Methods

    public int GetScore()
    {
        return score;
    }

    public void ResetScore()
    {
        score = baseScore;

        UIManager.Instance.UpdateScoreText(score);
    }

    public void IncrementScore()
    {
        score += scoreIncrementValue;

        UIManager.Instance.UpdateScoreText(score);
    }

    public void DecrementScore()
    {
        score -= scoreDecrementValue;
        
        if (score <= 0)
        {
            score = 0;
            onLose?.Invoke();
        }

        UIManager.Instance.UpdateScoreText(score);
    }

    #endregion
}
