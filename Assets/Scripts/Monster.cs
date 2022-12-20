using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    [SerializeField]
    private int hitpoints = 1;

    protected float moveRange;
    [SerializeField]
    protected float moveSpeed = 3f;
    protected Vector2 moveStartPos;
    protected Vector2 moveTargetPos;
    protected float moveStartTime;
    protected float moveDuration;

    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Sprite[] sprites;
    [SerializeField]
    private float spriteChangeSpeed = 0.25f;

    protected virtual void Start()
    {
        ChangeTargetPos(true);
        StartMove();
        StartCoroutine(ChangeSprite());
    }

    protected virtual void Update()
    {
        Move();
    }

    protected IEnumerator ChangeSprite()
    {
        for (int i = 0; ; i = i == sprites.Length - 1 ? 0 : i + 1)
        {
            spriteRenderer.sprite = sprites[i];
            yield return new WaitForSeconds(spriteChangeSpeed);
        }
    }

    protected virtual void Move()
    {
        float u = (Time.time - moveStartTime) / moveDuration;
        transform.position = Vector2.Lerp(moveStartPos, moveTargetPos, u);
        if (u >= 1)
        {
            ChangeTargetPos();
            StartMove();
        }
    }

    protected virtual void ChangeTargetPos(bool firstCall = false)
    {
        if (firstCall || transform.position.x >= moveRange)
            moveTargetPos = new Vector2(-moveRange, transform.position.y);
        else if (transform.position.x <= -moveRange)
            moveTargetPos = new Vector2(moveRange, transform.position.y);
    }

    protected virtual void StartMove()
    {
        moveStartPos = transform.position;
        moveDuration = Vector2.Distance(transform.position, moveTargetPos) / moveSpeed;
        moveStartTime = Time.time;
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

    protected bool IsMonsterInCameraWidth(Vector2 pos)
    {
        return pos.x < Camera.main.orthographicSize * Camera.main.aspect - transform.localScale.x ||
        pos.x > -(Camera.main.orthographicSize * Camera.main.aspect) + transform.localScale.x;
    }
}
