using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePlatform : Platform, IMoveable
{
    private float moveRange;
    [SerializeField] private Mover mover;

    protected override void Start()
    {
        base.Start();
        moveRange = Camera.main.orthographicSize * Camera.main.aspect - transform.localScale.x / 2;
    }

    public void ChangeTargetPos(bool firstCall = false)
    {
        if (firstCall || transform.position.x <= -moveRange)
            mover.MoveTargetPos = new Vector2(moveRange, transform.position.y);
        else if (transform.position.x >= moveRange)
            mover.MoveTargetPos = new Vector2(-moveRange, transform.position.y);
    }
}
