using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propeller : MonoBehaviour
{
    [SerializeField]
    protected float duration = 5f;
    protected float startTime;
    [SerializeField]
    protected float flySpeed = 10f;

    protected void OnEnable()
    {
        startTime = Time.time;
    }

    protected virtual void Update()
    {
        if (Time.time - startTime >= duration)
        {
            gameObject.SetActive(false);
        }
        Player.S.Rigidbody.velocity = Vector2.up * flySpeed; 
    }
}
