using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

public class UIManager : MonoBehaviour
{
    #region Singleton

    public static UIManager Instance { get; private set; }

    #endregion

    #region Members

    [SerializeField] private Canvas canvas;

    [SerializeField] private GameObject background;
    [SerializeField] private GameObject startButton;

    [SerializeField] private GameObject gamePanel;

    [SerializeField] private GameObject preGamePanel;
    [SerializeField] private Text countdown_Text;

    [SerializeField] private GameObject inGamePanel;
    [SerializeField] private Text score_Text;
    [SerializeField] private Text timer_Text;
    
    [SerializeField] private GameObject endPanel;
    [SerializeField] private Text end_Text;

    [SerializeField] private RandomKeyPicker randomKeyPicker;

    [SerializeField, Range(0, 10)] private int countdownDuration = 3;
    

    [SerializeField] private float baseDelay = 5f;

    private Coroutine corCountdown;
    private Coroutine corDelayedReset;

    #endregion

    #region Monobehaviours

    private void Awake()
    {
        Instance = this;

        ResetUI();
    }

    #endregion

    #region Public Methods

    public void StartNewGame()
    {
        startButton.SetActive(false);
        gamePanel.SetActive(true);

        randomKeyPicker.StartPicker();
    }

    public void ResetUI()
    {
        DisableUI();

        // Enable selectively default items
        startButton.SetActive(true);
    }

    public void DisableUI()
    {
        // Disable global UI
        foreach (Transform tr in canvas.transform)
            tr.gameObject.SetActive(false);

        // Disable in game UI
        DisableInGameUI();

        // Keep background
        background.SetActive(true);
    }

    public void DisableInGameUI()
    {
        // Disable in game UI
        foreach (Transform tr in gamePanel.transform)
            tr.gameObject.SetActive(false);
    }

    public void ToggleInGameUI(bool enable)
    {
        inGamePanel.SetActive(enable);
    }

    public void UpdateScoreText(int newValue)
    {
        score_Text.text = newValue.ToString("N0", new CultureInfo("is-IS"));
    }

    public void DisplayFinalScoreText(int newValue)
    {
        if (newValue != 0)
            end_Text.text = "Congratulations ! Your score is : " + newValue.ToString("N0", new CultureInfo("is-IS"));
        else
            end_Text.text = "Game Over !";

        DisableInGameUI();

        endPanel.SetActive(true);
    }

    public void UpdateTimerText(int newValue)
    {
        timer_Text.text = newValue.ToString("N0", new CultureInfo("is-IS")) + "s";
    }

    public void StartCountdown(Action callback)
    {
        if (corCountdown != null) StopCoroutine(corCountdown);
        corCountdown = StartCoroutine(Countdown(callback));
    }

    public void StopCountdown()
    {
        if (corCountdown != null) StopCoroutine(corCountdown);
        corCountdown = null;

        preGamePanel.SetActive(false);
        countdown_Text.text = "";
    }

    #endregion

    #region Private Methods

    private IEnumerator Countdown(Action callback)
    {
        int currentCount = 0;

        preGamePanel.SetActive(true);
        countdown_Text.text = (countdownDuration - currentCount).ToString();

        while (currentCount < countdownDuration)
        {
            yield return new WaitForSecondsRealtime(1f);

            currentCount++;
            countdown_Text.text = (countdownDuration - currentCount).ToString();
        }

        preGamePanel.SetActive(false);

        callback();
    }

    #endregion
}
