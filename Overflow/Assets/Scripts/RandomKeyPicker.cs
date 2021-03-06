﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RandomKeyPicker : MonoBehaviour
{
    #region Members

    [SerializeField, Range(0f, 15f)] private float timerTickMin = 2f;
    [SerializeField, Range(0f, 15f)] private float timerTickExtra = 2f;

    [field: SerializeField, Range(1, 240)] public int duration { get; private set; } = 60;

    [SerializeField] private Image keyToPress_Img;

    [SerializeField] private Keys keys; // Scriptable Object

    private Keys.SpriteKeyCode selectedKey;

    private Coroutine corRandomKey;
    private bool shouldStart = false;

    private bool correctInput = false;

    #endregion

    #region Monobehaviors

    private void OnEnable()
    {
        InputManager.keyPressed += CheckKeyPress;
        InputManager.endOfFrame += EndOfInputFrame;

        ScoreManager.onLose += EndOfGame;
    }

    private void OnDisable()
    {
        InputManager.keyPressed -= CheckKeyPress;
        InputManager.endOfFrame -= EndOfInputFrame;

        ScoreManager.onLose -= EndOfGame;
    }

    private void LateUpdate()
    {
        correctInput = false; // Reset current input after end of frame
    }

    #endregion

    #region Public Methods

    public void StartPicker()
    {
        shouldStart = true;

        UIManager.Instance.StartCountdown(StartRandomKeyPicker);
    }

    public void StopPicker()
    {
        shouldStart = false;

        UIManager.Instance.StopCountdown();

        StopRandomKeyPicker();
    }

    #endregion

    #region Private Methods

    private void CheckKeyPress(KeyCode code)
    {
        if (corRandomKey != null && code == selectedKey.keyCode)
        {
            correctInput = true;

            ScoreManager.Instance.IncrementScore();

            //Debug.Log("CORRECT KEY!");
        }

        if (code == KeyCode.Escape)
        {
            StopPicker();

            UIManager.Instance.ResetUI();
            ScoreManager.Instance.ResetScore();
        }
    }

    private void EndOfInputFrame()
    {
        if (corRandomKey != null && !correctInput)
        {
            ScoreManager.Instance.DecrementScore();
        }
    }

    private void EndOfGame()
    {
        StopPicker();

        UIManager.Instance.DisplayFinalScoreText(ScoreManager.Instance.GetScore());

        ScoreManager.Instance.ResetScore();
    }

    private void StartRandomKeyPicker()
    {
        if (!shouldStart) return;

        if (corRandomKey != null) StopCoroutine(corRandomKey);

        UIManager.Instance.ToggleInGameUI(true);
        corRandomKey = StartCoroutine(PickRandomKey());
    }

    private void StopRandomKeyPicker()
    {
        if (corRandomKey != null) StopCoroutine(corRandomKey);

        UIManager.Instance.UpdateTimerText(duration);

        corRandomKey = null;
    }

    private IEnumerator PickRandomKey()
    {
        float timerCurrent = 0f;
        float tickerCurrent = 0f;
        float extraTimeRandom = 0f;

        UIManager.Instance.UpdateTimerText(Mathf.CeilToInt(duration - timerCurrent));

        // Pick a random key from the start (To avoid having a blank key)

        // Pick a new random sprite
        // Avoids picking the same sprite twice
        selectedKey = keys.spritesKeyCodes.Where(x => x.sprite != keyToPress_Img.sprite).ToList()[Random.Range(0, keys.spritesKeyCodes.Count - 1)];

        // Assign the key to our sprite
        keyToPress_Img.sprite = selectedKey.sprite;

        while (timerCurrent < duration)
        {
            timerCurrent += Time.deltaTime;
            tickerCurrent += Time.deltaTime;

            if (tickerCurrent >= timerTickMin + extraTimeRandom)
            {
                tickerCurrent %= timerTickMin + extraTimeRandom;

                extraTimeRandom = Random.Range(0f, timerTickExtra);

                // Pick a new random sprite
                // Avoids picking the same sprite twice
                selectedKey = keys.spritesKeyCodes.Where(x => x.sprite != keyToPress_Img.sprite).ToList()[Random.Range(0, keys.spritesKeyCodes.Count - 1)];

                // Assign the key to our sprite
                keyToPress_Img.sprite = selectedKey.sprite;
            }

            UIManager.Instance.UpdateTimerText(Mathf.CeilToInt(duration - timerCurrent));

            yield return null;
        }

        EndOfGame();
    }

    #endregion
}
