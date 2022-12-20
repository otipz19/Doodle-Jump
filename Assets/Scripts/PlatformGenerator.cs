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

    private int startPlatformsCount = 20;
    [SerializeField]
    private int maxPlatformsCount = 50;
    private int curPlatformsCount;

    private float previousLayerY;

    [SerializeField]
    private float minLayersDistance = 1f;
    [SerializeField]
    private float maxLayersDistance = 4f;
    [SerializeField]
    private float minPlatformHorizontalDistance = 1.4f;
    [SerializeField]
    private float platformsInLayerDiffusion = 0.25f;
    [SerializeField]
    private int maxPlatformsInLayer = 2;

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
        previousLayerY = Player.S.transform.position.y - 5;
        FirstGeneration();
        StartCoroutine(ContinuousGeneration());
    }

    private float GeneratePlatform(float layerY, float? previousPlatformX)
    {
        GameObject platform = Instantiate<GameObject>(platformPrefab);
        Vector2 platformPos = Vector2.zero;
        if(curPlatformsCount == 0)
        {
            platformPos = new Vector2(Player.S.transform.position.x, Player.S.transform.position.y - 0.5f);
        }
        else
        {
            do
            {
                platformPos = new Vector2(Random.Range(-horizontalRange, horizontalRange),
                                          layerY + Random.Range(-platformsInLayerDiffusion, platformsInLayerDiffusion));
            }
            while (previousPlatformX != null && Mathf.Abs((float)previousPlatformX - platformPos.x) < minPlatformHorizontalDistance);
        }
        platform.transform.position = platformPos;
        platform.transform.parent = platformsParent;
        curPlatformsCount++;
        return platformPos.x;
    }

    private void GenerateLayer()
    {
        float layerY = Random.Range(previousLayerY + minLayersDistance, previousLayerY + maxLayersDistance);
        float? previousPlatformX = null;
        int platformsInLayer = Random.Range(1, maxPlatformsInLayer + 1);
        for (int i = 0; i < platformsInLayer; i++)
        {
            previousPlatformX = GeneratePlatform(layerY, previousPlatformX);
        }
        previousLayerY = layerY;
    }

    private void FirstGeneration()
    {
        for (int i = 0; i < startPlatformsCount; i++)
        {
            GenerateLayer();
        }
    }

    private IEnumerator ContinuousGeneration()
    {
        while(true)
        {
            if(curPlatformsCount < maxPlatformsCount)
            {
                GenerateLayer();
            }
            yield return new WaitForSeconds(1f);
        }
    }

    public void PlatformDestroyed()
    {
        curPlatformsCount--;
    }
}
