using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Members

    [SerializeField] private GameObject keyToPress;
    [SerializeField] private GameObject scorePanel;
    [SerializeField] private GameObject winPanel;

    [SerializeField] private RandomKeyPicker randomKeyPicker;

    #endregion

    #region Monobehaviours

    private void Awake()
    {
        // Disable entire UI
        foreach (Transform tr in transform)
            tr.gameObject.SetActive(false);

        scorePanel.SetActive(true);
        keyToPress.SetActive(true);

        // Start our picker
        randomKeyPicker.StartRandomKeyPicker();
    }

    #endregion
}
