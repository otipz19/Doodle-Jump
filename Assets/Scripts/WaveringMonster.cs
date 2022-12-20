using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveringMonster : Monster
{
    private Vector2 spawnPos;

    private void Start()
    {
        spawnPos = transform.position;
        ChangeTargetPos();
        StartMove();
    }

    protected override void ChangeTargetPos(bool firstCall = false)
    {
        moveTargetPos = spawnPos + (Vector2)Random.onUnitSphere * moveRange;
        float border;
        if (moveTargetPos.x < (border = -Camera.main.orthographicSize * Camera.main.aspect + transform.localScale.x))
            moveTargetPos = new Vector2(border + moveRange / 4, moveTargetPos.y);
        else if (moveTargetPos.x > (border = Camera.main.orthographicSize * Camera.main.aspect - transform.localScale.y))
            moveTargetPos = new Vector2(border - moveRange / 4, moveTargetPos.y);
    }
}
