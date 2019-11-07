using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Members

    [SerializeField, Range(1, 240)] private int targetFPS = 60;

    #endregion

    #region Monobehaviors

    private void Awake()
    {
        // Quality settings
        QualitySettings.vSyncCount = 0; // Disable VSync
        Application.targetFrameRate = targetFPS; // Set target FPS
    }

    #endregion
}
