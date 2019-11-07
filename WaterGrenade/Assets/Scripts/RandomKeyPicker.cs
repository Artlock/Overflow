using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RandomKeyPicker : MonoBehaviour
{
    #region Members

    [SerializeField, Range(0f, 15f)] private float timerTickMin = 2f;
    [SerializeField, Range(0f, 15f)] private float timerTickExtra = 2f;

    [SerializeField] private Image keyToPress_Img;
    [SerializeField] private Keys letters;

    private Sprite selectedKey;

    private Coroutine corRandomKey;

    #endregion

    #region Public Methods

    public void StartRandomKeyPicker()
    {
        if (corRandomKey != null) StopCoroutine(corRandomKey);
        corRandomKey = StartCoroutine(PickRandomKey());
    }

    public void StopRandomKeyPicker()
    {
        if (corRandomKey != null) StopCoroutine(corRandomKey);
        corRandomKey = null;
    }

    #endregion

    #region Private Methods

    private IEnumerator PickRandomKey()
    {
        float timerCurrent = 0f;
        float extraTimeRandom = 0f;

        while (true)
        {
            timerCurrent += Time.deltaTime;

            if (timerCurrent >= timerTickMin + extraTimeRandom)
            {
                timerCurrent %= timerTickMin + extraTimeRandom;

                extraTimeRandom = Random.Range(0f, timerTickExtra);

                // Pick a new random sprite
                // Avoids picking the same sprite twice
                keyToPress_Img.sprite = letters.sprites.Where(x => x != keyToPress_Img.sprite).ToList()[Random.Range(0, letters.sprites.Count - 1)];
            }

            yield return null;
        }       
    }

    #endregion
}
