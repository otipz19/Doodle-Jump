using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("Generation settings")]
    [SerializeField] private SpawnAreaPrefab[] smallSpawnAreasPrefabs;
    [SerializeField] private SpawnAreaPrefab[] bigSpawnAreasPrefabs;
    [SerializeField] private int minBigSpawnAreas = 1;
    [SerializeField] private int maxBigSpawnAreas = 3;

    private SpawnArea[,] matrix;
    private int matrixWidth = 3;
    [SerializeField] private int matrixHeight = 9;
    private int curHeight;
    private float xCoef = 1.875f;
    private float yCoef = 2f;

    private Vector2 MatrixToScenePoint(Vector2Int matrixPos) => new Vector2((matrixPos.x - 1) * xCoef, matrixPos.y * yCoef);

    private void Start()
    {
        matrix = new SpawnArea[matrixHeight, matrixWidth];
        for (int i = 0; i < 2; i++)
            GenerateLayer();
    }

    private void Update()
    {
        if (!ScoreManager.S.IsGameOver && curHeight * xCoef - Player.S.transform.position.y < matrixHeight)
            GenerateLayer();
    }

    private void GenerateLayer()
    {
        FillMatrix();
        SpawnObjects();
        curHeight += matrixHeight;
    }

    private void FillMatrix()
    {
        //Filling matrix with small spawn areas
        for (int y = 0; y < matrixHeight; y++)
            for (int x = 0; x < matrixWidth; x++)
            {
                matrix[y, x] = new SpawnArea() { Prefab = smallSpawnAreasPrefabs[Random.Range(0, smallSpawnAreasPrefabs.Length - 1)] };
            }

        //Filling matrix with big spawn areas
        int bigSpawnAreasCount = Random.Range(minBigSpawnAreas, maxBigSpawnAreas + 1);
        for (int i = 0; i < bigSpawnAreasCount; i++)
        {
            SpawnArea curToSpawn = new SpawnArea() { Prefab = bigSpawnAreasPrefabs[Random.Range(0, bigSpawnAreasPrefabs.Length - 1)] };
            curToSpawn.StartPos = new Vector2Int(0, Random.Range(0, matrixHeight - 4));
            if(CheckSpawnAreaIntercepts(curToSpawn.StartPos, curToSpawn.EndPos))
                ResolveInterception(curToSpawn);
            for (int y = curToSpawn.StartPos.y; y <= curToSpawn.EndPos.y; y++)
                for (int x = curToSpawn.StartPos.x; x <= curToSpawn.EndPos.x; x++)
                {
                    matrix[y, x] = curToSpawn;
                }
        }
    }

    private void ResolveInterception(SpawnArea spawnArea)
    {
        if (spawnArea.Prefab.Scale.x > 1)
        {
            for (int y = 0; y < matrixHeight; y++)
            {
                spawnArea.StartPos = new Vector2Int(spawnArea.StartPos.x, y);
                if (!CheckSpawnAreaIntercepts(spawnArea.StartPos, spawnArea.EndPos))
                    break;
            }
        }
        else if(spawnArea.Prefab.Scale.y > 1)
        {
            for (int y = 0; y < matrixHeight - spawnArea.Prefab.Scale.y; y += spawnArea.Prefab.Scale.y)
                for (int x = 0; x < matrixWidth; x++)
                {
                    spawnArea.StartPos = new Vector2Int(x, y);
                    if (!CheckSpawnAreaIntercepts(spawnArea.StartPos, spawnArea.EndPos))
                        break;
                }
        }
    }

    private bool CheckSpawnAreaIntercepts(Vector2Int startPos, Vector2Int endPos)
    {
        for (int y = startPos.y; y <= endPos.y; y++)
            for (int x = startPos.x; x <= endPos.x; x++)
            {
                if (matrix[y, x].Prefab.IsBig)
                    return true;
            }
        return false;
    }

    private void SpawnObjects()
    {
        for (int y = 0; y < matrixHeight; y++)
            for (int x = 0; x < matrixWidth; x++)
            {
                matrix[y, x].Spawn(MatrixToScenePoint(new Vector2Int(x, curHeight + y)));
            }
    }
}
