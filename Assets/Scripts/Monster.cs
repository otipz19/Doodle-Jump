using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    protected Mover mover;
    protected float moveRange;

    [SerializeField]
    protected SpriteRenderer spriteRenderer;
    [SerializeField]
    private Sprite[] sprites;
    [SerializeField]
    private float spriteChangeSpeed = 0.25f;

    [SerializeField]
    private int hitpoints = 1;

    private void Start()
    {
        mover = GetComponent<Mover>();
        Initialize();
    }

    protected abstract void Initialize();

    protected IEnumerator ChangeSpritesContinuously()
    {
        for (int i = 0; ; i = i == sprites.Length - 1 ? 0 : i + 1)
        {
            spriteRenderer.sprite = sprites[i];
            yield return new WaitForSeconds(spriteChangeSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                Destroy(other.gameObject);
                break;
            case "PlayerBullet":
                if (--hitpoints <= 0)
                {
                    Destroy(this.gameObject);
                    Destroy(other.gameObject);
                }
                break;
        }
    }
}
