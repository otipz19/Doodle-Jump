using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveringMonster : Monster
{
    [SerializeField]
    private float randomMoveRadius = 1f;
    private Vector3 startPos;

    protected override void Start()
    {
        startPos = transform.position;
        ChangeTargetPos();
        StartMove();
    }

    protected override void ChangeTargetPos(bool firstCall = false)
    {
        do
        {
            moveTargetPos = Random.onUnitSphere * randomMoveRadius + startPos;
        }
        while (!IsMonsterInCameraWidth(moveTargetPos));
    }
}
