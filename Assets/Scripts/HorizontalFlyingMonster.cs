using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalFlyingMonster : Monster
{
    private void Start()
    {
        moveRange = Camera.main.orthographicSize * Camera.main.aspect - transform.localScale.x / 2;
        ChangeTargetPos(true);
        StartMove();
        StartCoroutine(ChangeSprite());
    }

    protected override void ChangeTargetPos(bool firstCall = false)
    {
        if (firstCall || transform.position.x >= moveRange)
            moveTargetPos = new Vector2(-moveRange, transform.position.y);
        else if (transform.position.x <= -moveRange)
            moveTargetPos = new Vector2(moveRange, transform.position.y);
    }
}
