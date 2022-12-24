using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField]
    private float duration = 10f;
    private float startTime;

    [SerializeField]
    private float blinkingIntensityMax = 1f;
    [SerializeField]
    private float blinkingIntensityMin = 0.1f;
    private float curBlinkTime;
    private float blinkDeltaTime;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Sprite[] sprites;
    private int curSpriteIndex;

    private void Update()
    {
        if (Time.time - startTime >= duration)
        {
            gameObject.SetActive(false);
            StopCoroutine(Blink());
        }
    }

    private void OnEnable()
    {
        startTime = Time.time;
        StartCoroutine(Blink());
    }

    private IEnumerator Blink()
    {
        blinkDeltaTime = blinkingIntensityMax / duration;
        curBlinkTime = blinkingIntensityMax;
        while (true)
        {
            if (curSpriteIndex != sprites.Length)
            {
                spriteRenderer.sprite = sprites[curSpriteIndex++];
                yield return new WaitForSeconds(blinkingIntensityMin);
            }
            else
            {
                spriteRenderer.sprite = sprites[curSpriteIndex = 0];
                yield return new WaitForSeconds(curBlinkTime -= blinkDeltaTime);
            }
        }
    }
}
