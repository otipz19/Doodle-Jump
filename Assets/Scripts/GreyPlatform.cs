using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreyPlatform : Platform, IMoveable
{
    [SerializeField] private Mover mover;
    [SerializeField] private float moveRange = 5f;
    private float startY;

    protected override void Start()
    {
        base.Start();
        startY = transform.position.y;
    }

    public void ChangeTargetPos(bool firstCall = false)
    {
        if (firstCall || transform.position.y <= startY)
            mover.MoveTargetPos = new Vector2(transform.position.x, startY + moveRange);
        else if (transform.position.y >= startY + moveRange)
            mover.MoveTargetPos = new Vector2(transform.position.x, startY);
    }
}
