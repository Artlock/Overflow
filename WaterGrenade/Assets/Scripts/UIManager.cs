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

    [SerializeField] private GameObject keyToPress;
    [SerializeField] private GameObject scorePanel;
    [SerializeField] private Text scorePanel_Text;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private Text endPanel_Text;
    [SerializeField] private GameObject timerPanel;
    [SerializeField] private Text timerPanel_Text;

    [SerializeField] private RandomKeyPicker randomKeyPicker;

    [SerializeField, Range(0, 10)] private int countdownDuration = 3;
    [SerializeField] private GameObject countdown;
    [SerializeField] private Text countdown_Text;

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
        // Disable entire UI
        foreach (Transform tr in canvas.transform)
            tr.gameObject.SetActive(false);

        // Keep background
        background.SetActive(true);
    }

    public void ToggleInGameUI(bool enable)
    {
        scorePanel.SetActive(enable);
        keyToPress.SetActive(enable);
        timerPanel.SetActive(enable);
    }

    public void UpdateScoreText(int newValue)
    {
        scorePanel_Text.text = newValue.ToString("N0", new CultureInfo("is-IS"));
    }

    public void DisplayFinalScoreText(int newValue)
    {
        if (newValue != 0)
            endPanel_Text.text = "Congratulations ! Your score is : " + newValue.ToString("N0", new CultureInfo("is-IS"));
        else
            endPanel_Text.text = "Game Over !";

        DisableUI();

        endPanel.SetActive(true);
    }

    public void UpdateTimerText(int newValue)
    {
        timerPanel_Text.text = newValue.ToString("N0", new CultureInfo("is-IS")) + "s";
    }

    public void DelayedReset()
    {
        if (corDelayedReset != null) StopCoroutine(corDelayedReset);
        corDelayedReset = StartCoroutine(ResetAfterDelay());
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
    }

    #endregion

    #region Private Methods

    private IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSecondsRealtime(baseDelay);

        ResetUI();
    }

    private IEnumerator Countdown(Action callback)
    {
        int currentCount = 0;

        countdown.SetActive(true);
        countdown_Text.text = (countdownDuration - currentCount).ToString();

        while (currentCount < countdownDuration)
        {
            yield return new WaitForSecondsRealtime(1f);

            currentCount++;
            countdown_Text.text = (countdownDuration - currentCount).ToString();
        }

        countdown.SetActive(false);

        callback();
    }

    #endregion
}
