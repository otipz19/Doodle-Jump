using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    protected float moveRange;
    [SerializeField]
    protected float moveSpeed = 3f;
    protected Vector2 moveStartPos;
    protected Vector2 moveTargetPos;
    protected float moveStartTime;
    protected float moveDuration;

    [SerializeField]
    protected SpriteRenderer spriteRenderer;
    [SerializeField]
    private Sprite[] sprites;
    [SerializeField]
    private float spriteChangeSpeed = 0.25f;

    [SerializeField]
    private int hitpoints = 1;

    protected abstract void ChangeTargetPos(bool firstCall = false);

    protected IEnumerator ChangeSprite()
    {
        for (int i = 0; ; i = i == sprites.Length - 1 ? 0 : i + 1)
        {
            spriteRenderer.sprite = sprites[i];
            yield return new WaitForSeconds(spriteChangeSpeed);
        }
    }

    protected void StartMove()
    {
        moveStartPos = transform.position;
        moveDuration = Vector2.Distance(transform.position, moveTargetPos) / moveSpeed;
        moveStartTime = Time.time;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float u = (Time.time - moveStartTime) / moveDuration;
        transform.position = Vector2.Lerp(moveStartPos, moveTargetPos, u);
        if (u >= 1)
        {
            ChangeTargetPos();
            StartMove();
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
