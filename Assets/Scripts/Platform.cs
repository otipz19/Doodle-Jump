using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    protected virtual void Update()
    {
        if (transform.position.y < Camera.main.transform.position.y - Camera.main.orthographicSize - 0.25f)
        {
            //PlatformGenerator.S.PlatformDestroyed();
            Destroy(this.gameObject);
        }
    }
}
