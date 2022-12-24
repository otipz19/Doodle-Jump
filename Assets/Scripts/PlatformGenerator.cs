using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    static private PlatformGenerator s;
    static public PlatformGenerator S => s;

    [SerializeField]
    private GameObject platformPrefab;
    private float horizontalRange;
    private Transform platformsParent;

    [SerializeField] 
    private float minPlatformsDistanceY = 1f;
    [SerializeField]
    private float maxPlatformsDistanceY = 2.5f;

    private void Awake()
    {
        if (s != null)
            throw new System.ApplicationException("PlatformGeneratos.S is already asigned");
        s = this;
    }

    private void Start()
    {
        horizontalRange = Camera.main.orthographicSize * Camera.main.aspect - platformPrefab.transform.localScale.x;
        platformsParent = new GameObject("Platforms Parent").transform;
    }

    public float GeneratePlatforms(float prevY, int platformsToGenerate)
    {
        for (int i = 0; i < platformsToGenerate; i++)
        {
            prevY = GeneratePlatform(prevY);
        }
        return prevY;
    }

    private float GeneratePlatform(float prevY)
    {
        GameObject platform = Instantiate<GameObject>(platformPrefab);
        platform.transform.position = new Vector2(Random.Range(-horizontalRange, horizontalRange),
                                          prevY + Random.Range(minPlatformsDistanceY, maxPlatformsDistanceY));
        platform.transform.parent = platformsParent;
        return platform.transform.position.y;
    }
}
