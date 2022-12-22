using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringBoots : MonoBehaviour
{
    public float JumpMod => jumpMod;
    [SerializeField]
    private float jumpMod = 2f;

    [SerializeField]
    private GameObject springBootsGM;
    [SerializeField]
    private float duration = 5f;
    private float startTime;

    [SerializeField]
    private float spriteChangeIntensity = 0.1f;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Sprite[] sprites;
    public bool FlipX { set => spriteRenderer.flipX = value; }

    private void Update()
    {
        if (Time.time - startTime >= duration)
        {
            Player.S.IsSpringBootsActive = false;
            springBootsGM.SetActive(false);
        }
    }

    public void Activate()
    {
        Player.S.IsSpringBootsActive = true;
        springBootsGM.SetActive(true);
        startTime = Time.time;
    }

    public IEnumerator Animate()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            spriteRenderer.sprite = sprites[i];
            yield return new WaitForSeconds(spriteChangeIntensity);
        }
        yield break;
    }
}
