using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject[] levelTemplates;
    [SerializeField]
    private float templateSpawnChance = 1f;
    [SerializeField]
    private int framesAtStart = 4;
    [SerializeField]
    private int minLayersToGenerate = 4;
    [SerializeField]
    private int maxLayersToGenerate = 8;

    private int prevTemplateIndex;
    private float prevY;

    private void Start()
    {
        prevY = -Camera.main.orthographicSize * 2;
        for (int i = 0; i < framesAtStart; i++)
        {
            GenerateFrame();
        }
    }

    private void Update()
    {
        if (ScoreManager.S.IsGameOver)
            return;
        if(prevY - Player.S.transform.position.y <= Camera.main.orthographicSize * 2)
        {
            GenerateFrame();
        }
    }

    private void GenerateFrame()
    {
        if(Random.value > templateSpawnChance)
        {
            prevY = PlatformGenerator.S.GeneratePlatforms(prevY + Camera.main.orthographicSize, Random.Range(minLayersToGenerate, maxLayersToGenerate + 1));
            prevY -= Camera.main.orthographicSize;
        }
        else
        {
            SpawnTemplate();
        }
    }

    private void SpawnTemplate()
    {
        int nextTemplateIndex;
        do
        {
            nextTemplateIndex = Random.Range(0, levelTemplates.Length);
        }
        while (nextTemplateIndex == prevTemplateIndex);
        prevTemplateIndex = nextTemplateIndex;

        GameObject frame = Instantiate<GameObject>(levelTemplates[nextTemplateIndex]);
        frame.transform.position = new Vector2(0, prevY += Camera.main.orthographicSize * 2);
    }
}
