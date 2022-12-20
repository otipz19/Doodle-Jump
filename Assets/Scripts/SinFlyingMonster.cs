using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinFlyingMonster : Monster
{
    [SerializeField]
    private float sinMod = 2.5f;
    private List<Vector2> path = new List<Vector2>();
    private int pointsInPath = 50;
    private int curPoint;
    private System.Func<int, int> curDirection;
    private System.Func<int, int> rightDirection = (x) => ++x;
    private System.Func<int, int> leftDirection = (x) => --x;

    private void Start()
    {
        moveRange = Camera.main.orthographicSize * Camera.main.aspect - transform.localScale.x / 2;
        transform.position = new Vector2(-moveRange, transform.position.y);
        ChangeTargetPos(true);
        StartMove();
        StartCoroutine(ChangeSprite());
    }

    protected override void ChangeTargetPos(bool firstCall = false)
    {
        if (firstCall)
        {
            float deltaX = moveRange * 2 / pointsInPath;
            for (float x = -moveRange; x <= moveRange; x += deltaX)
            {
                path.Add(new Vector2(x, Mathf.Sin(x * sinMod)));
            }
        }
        if (curPoint == 0)
            curDirection = rightDirection;
        else if (curPoint == pointsInPath)
            curDirection = leftDirection;
        moveTargetPos = path[curPoint = curDirection(curPoint)];
    }
}
