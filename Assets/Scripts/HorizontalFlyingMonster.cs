using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalFlyingMonster : Monster
{
    protected override void Start()
    {
        moveRange = Camera.main.orthographicSize * Camera.main.aspect - transform.localScale.x / 2;
        base.Start();
    }
}
