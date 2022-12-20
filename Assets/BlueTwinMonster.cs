using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueTwinMonster : Monster
{
    [SerializeField]
    private GameObject twinPrefab;
    private BlueTwinMonster twin;

    private float edgeX;
    private float centralX;

    private void Start()
    {
        moveRange = Camera.main.orthographicSize * Camera.main.aspect - transform.localScale.x / 2;
        if(twin == null)
        {
            transform.position = new Vector2(-moveRange, transform.position.y);
            twin = Instantiate(twinPrefab).GetComponent<BlueTwinMonster>();
            twin.transform.position = new Vector2(moveRange, transform.position.y);
            twin.twin = this;
        }
        edgeX = transform.position.x;
        centralX = edgeX < 0 ? -transform.localScale.x / 2 : transform.localScale.x / 2;
        ChangeTargetPos(true);
        StartMove();
    }

    protected override void ChangeTargetPos(bool firstCall = false)
    {
        float targetX = 0;
        if (firstCall || twin != null && Mathf.Approximately(transform.position.x, edgeX))
        {
            targetX = centralX;
        }
        else if (twin != null && Mathf.Approximately(transform.position.x, centralX))
        {
            targetX = edgeX;
        }
        else if (twin == null && Mathf.Approximately(transform.position.x, edgeX))
        {
            targetX = -edgeX;
        }
        else if (twin == null && Mathf.Approximately(transform.position.x, -edgeX))
        {
            targetX = edgeX;
        }
        moveTargetPos = new Vector2(targetX, transform.position.y);
        spriteRenderer.flipX = targetX < transform.position.x;
    }
}
