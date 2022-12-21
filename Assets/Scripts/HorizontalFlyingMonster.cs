using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalFlyingMonster : Monster, IMoveable
{
    private void Start()
    {
        mover = new Mover(this, moveSpeed);
        moveRange = Camera.main.orthographicSize * Camera.main.aspect - transform.localScale.x / 2;
        ChangeTargetPos(true);
        mover.StartMove();
        StartCoroutine(ChangeSpritesContinuously());
    }

    public void ChangeTargetPos(bool firstCall = false)
    {
        if (firstCall || transform.position.x >= moveRange)
            mover.MoveTargetPos = new Vector2(-moveRange, transform.position.y);
        else if (transform.position.x <= -moveRange)
            mover.MoveTargetPos = new Vector2(moveRange, transform.position.y);
    }
}
