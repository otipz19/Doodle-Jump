using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolatilePlatform : Platform
{
    [SerializeField] private SpriteChanger spriteChanger;
    [SerializeField] private new Collider2D collider;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.relativeVelocity.y <= 0)
        {
            collider.enabled = false;
            StartCoroutine(spriteChanger.DieAnimation());
        }
    }
}
