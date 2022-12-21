using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpableSurface : MonoBehaviour
{
    [SerializeField]
    private float speedModifier = 1f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.relativeVelocity.y <= 0)
        {
            Player.S.Jump(speedModifier);
        }
    }
}
