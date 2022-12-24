using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 15f;
    [SerializeField]
    private float maxLifeTimeInSeconds = 5f;
    private float lifeStarted;

    private void Awake()
    {
        lifeStarted = Time.time;
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
        if (Time.time - lifeStarted >= maxLifeTimeInSeconds)
            Destroy(this.gameObject);
    }
}
