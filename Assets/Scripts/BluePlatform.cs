using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePlatform : Platform, IMoveable
{
    private float moveRange;
    private Mover mover;

    private void Start()
    {
        moveRange = Camera.main.orthographicSize * Camera.main.aspect - transform.localScale.x / 2;
        mover = GetComponent<Mover>();
    }

    public void ChangeTargetPos(bool firstCall = false)
    {
        if (firstCall || transform.position.x <= -moveRange)
            mover.MoveTargetPos = new Vector2(moveRange, transform.position.y);
        else if (transform.position.x >= moveRange)
            mover.MoveTargetPos = new Vector2(-moveRange, transform.position.y);
    }
}
