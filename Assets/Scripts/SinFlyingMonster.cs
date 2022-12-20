using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinFlyingMonster : Monster
{
    protected override void Move()
    {
        base.Move();
        transform.position = new Vector2(transform.position.x, transform.position.y + Mathf.Sin(Time.time));
    }
}
