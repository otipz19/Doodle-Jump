using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Sprite[] sprites;
    [SerializeField]
    private float spriteChangeSpeed = 0.25f;
    public float SpriteChangeSpeed
    {
        get => spriteChangeSpeed;
        set => spriteChangeSpeed = value;
    }

    public IEnumerator LoopAnimation()
    {
        for (int i = 0; ; i = i == sprites.Length - 1 ? 0 : i + 1)
        {
            spriteRenderer.sprite = sprites[i];
            yield return new WaitForSeconds(spriteChangeSpeed);
        }
    }

    public IEnumerator DieAnimation(float changeSpeedAcceleration = 1f)
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            spriteRenderer.sprite = sprites[i];
            yield return new WaitForSeconds(spriteChangeSpeed / changeSpeedAcceleration);
        }
        Destroy(this.gameObject);
    }
}
