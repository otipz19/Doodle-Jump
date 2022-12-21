using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolatilePlatform : Platform
{
    private SpriteChanger spriteChanger;
    private new Collider2D collider;

    private void Start()
    {
        spriteChanger = GetComponent<SpriteChanger>();
        collider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.relativeVelocity.y <= 0)
        {
            collider.enabled = false;
            StartCoroutine(spriteChanger.DieAnimation());
        }
    }
}
