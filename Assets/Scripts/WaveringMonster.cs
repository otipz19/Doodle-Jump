using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveringMonster : Monster, IMoveable
{
    private Vector2 spawnPos;

    private void Start()
    {
        mover = new Mover(this, moveSpeed);
        moveRange = 1f;
        spawnPos = transform.position;
        ChangeTargetPos();
        mover.StartMove();
    }

    public void ChangeTargetPos(bool firstCall = false)
    {
        mover.MoveTargetPos = spawnPos + (Vector2)Random.onUnitSphere * moveRange;
        float border;
        if (mover.MoveTargetPos.x < (border = -Camera.main.orthographicSize * Camera.main.aspect + transform.localScale.x))
            mover.MoveTargetPos = new Vector2(border + moveRange / 4, mover.MoveTargetPos.y);
        else if (mover.MoveTargetPos.x > (border = Camera.main.orthographicSize * Camera.main.aspect - transform.localScale.y))
            mover.MoveTargetPos = new Vector2(border - moveRange / 4, mover.MoveTargetPos.y);
    }
}
