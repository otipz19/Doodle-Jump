using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveable
{
    void ChangeTargetPos(bool firstCall = false);
}

public class Mover : MonoBehaviour
{
    private IMoveable moveable;
    [SerializeField]
    private float moveSpeed;
    private Vector2 moveStartPos;
    private float moveStartTime;
    private float moveDuration;

    public Vector2 MoveTargetPos { get; set; }

    private void Start()
    {
        moveable = GetComponent<IMoveable>();
        moveable.ChangeTargetPos(true);
        StartMove();
    }

    private void Update()
    {
        float u = (Time.time - moveStartTime) / moveDuration;
        transform.position = Vector2.Lerp(moveStartPos, MoveTargetPos, u);
        if (u >= 1)
        {
            moveable.ChangeTargetPos();
            StartMove();
        }
    }

    public void StartMove()
    {
        moveStartPos = transform.position;
        moveDuration = Vector2.Distance(transform.position, MoveTargetPos) / moveSpeed;
        moveStartTime = Time.time;
    }
}
