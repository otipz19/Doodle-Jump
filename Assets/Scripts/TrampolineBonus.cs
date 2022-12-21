using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineBonus : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Sprite defaultState;
    [SerializeField]
    private Sprite[] activatedStates;
    [SerializeField]
    private float changeSpriteDuration = 0.1f;
    private int curSpriteIndex;

    private float startTime;
    private bool isChangingSprite;

    private void Update()
    {
        if (isChangingSprite && Time.time - startTime >= changeSpriteDuration)
        {
            curSpriteIndex++;
            if(curSpriteIndex >= activatedStates.Length)
            {
                spriteRenderer.sprite = defaultState;
                isChangingSprite = false;
            }
            else
            {
                spriteRenderer.sprite = activatedStates[curSpriteIndex];
                startTime = Time.time;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.relativeVelocity.y <= 0)
        {
            curSpriteIndex = 0;
            spriteRenderer.sprite = activatedStates[curSpriteIndex];
            startTime = Time.time;
            isChangingSprite = true;
        }
    }
}
