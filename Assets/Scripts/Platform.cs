using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private void Update()
    {
        if (transform.position.y < Camera.main.transform.position.y - Camera.main.orthographicSize - 0.25f)
        {
            PlatformGenerator.S.PlatformDestroyed();
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.relativeVelocity.y <= 0)
        {
            Player.S.Rigidbody.velocity = Vector2.up * Player.S.VerticalSpeed;
        }
    }
}
