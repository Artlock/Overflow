using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region Members

    public static event Action<KeyCode> keyPressed;
    public static event Action endOfFrame;

    [SerializeField] private Keys keys; // Scriptable Object

    #endregion

    #region Monobehaviors

    private void Update()
    {
        foreach (Keys.SpriteKeyCode k in keys.spritesKeyCodes)
        {
            if (Input.GetKey(k.keyCode)) keyPressed?.Invoke(k.keyCode);
        }

        endOfFrame?.Invoke();
    }

    #endregion
}
