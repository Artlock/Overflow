using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Members

    [SerializeField, Range(1, 240)] private int targetFPS = 60;

    [SerializeField] private GameObject keyToPress;
    [SerializeField] private Image keyToPress_Img;

    [SerializeField] private GameObject scorePanel;
    [SerializeField] private Text scorePanel_Text;

    [SerializeField] private GameObject winPanel;
    [SerializeField] private Text winPanel_Text;

    [SerializeField] private Keys letters;

    [SerializeField] private float timerTick = 2f;
    private float timerCurrent = 0f;

    #endregion

    #region Monobehaviours

    private void Awake()
    {
        // Quality settings
        QualitySettings.vSyncCount = 0; // Disable VSync
        Application.targetFrameRate = targetFPS; // Set target FPS

        // Disable entire UI
        foreach (Transform tr in transform)
            tr.gameObject.SetActive(false);

        scorePanel.SetActive(true);
        keyToPress.SetActive(true);
    }

    // Random letter every x seconds prototype
    private void Update()
    {
        timerCurrent += Time.deltaTime;

        if (timerCurrent >= timerTick)
        {
            timerCurrent %= timerTick;
            keyToPress_Img.sprite = letters.sprites[Random.Range(0, letters.sprites.Count - 1)];
        }
    }

    #endregion

    #region UI



    #endregion
}
