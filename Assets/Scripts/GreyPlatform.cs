using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreyPlatform : Platform, IMoveable
{
    private Mover mover;
    [SerializeField]
    private float maxMoveRange;
    [SerializeField]
    private float minMoveRange;
    private float moveRange;
    private float startY;

    private void Start()
    {
        mover = GetComponent<Mover>();
        moveRange = Random.Range(minMoveRange, maxMoveRange);
        startY = transform.position.y;
    }

    public void ChangeTargetPos(bool firstCall = false)
    {
        if (firstCall || transform.position.y <= startY - moveRange)
            mover.MoveTargetPos = new Vector2(transform.position.x, startY + moveRange);
        else if (transform.position.y >= startY + moveRange)
            mover.MoveTargetPos = new Vector2(transform.position.x, startY - moveRange);
    }
}
