using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearableObject : MonoBehaviour
{
    private void Update()
    {
        if (transform.position.y < Camera.main.transform.position.y - Camera.main.orthographicSize - 0.25f)
        {
            Destroy(this.gameObject);
        }
    }
}
