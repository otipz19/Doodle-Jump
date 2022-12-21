using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable
{
    void ChangeTargetPos(bool firstCall = false);
}

public class Mover 
{
    private MonoBehaviour moveable;
    public Vector2 MoveTargetPos { get; set; }
    private float moveSpeed;
    private Vector2 moveStartPos;
    private float moveStartTime;
    private float moveDuration;

    public Mover(IMoveable moveable, float moveSpeed)
    {
        this.moveable = (MonoBehaviour)moveable;
        this.moveSpeed = moveSpeed;
    }

    public void StartMove()
    {
        moveStartPos = moveable.transform.position;
        moveDuration = Vector2.Distance(moveable.transform.position, MoveTargetPos) / moveSpeed;
        moveStartTime = Time.time;
    }

    public void Move()
    {
        float u = (Time.time - moveStartTime) / moveDuration;
        moveable.transform.position = Vector2.Lerp(moveStartPos, MoveTargetPos, u);
        if (u >= 1)
        {
            ((IMoveable)moveable).ChangeTargetPos();
            StartMove();
        }
    }
}
