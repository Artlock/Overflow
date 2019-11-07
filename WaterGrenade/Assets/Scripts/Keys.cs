using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Keys", menuName = "Scriptable Object/Keys")]
public class Keys : ScriptableObject
{
    public List<SpriteKeyCode> spritesKeyCodes;

    [Serializable]
    public class SpriteKeyCode
    {
        [field: SerializeField] public Sprite sprite { get; private set; }
        [field: SerializeField] public KeyCode keyCode { get; private set; }
    }
}
