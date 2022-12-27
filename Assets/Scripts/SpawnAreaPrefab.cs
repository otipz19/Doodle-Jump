using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea
{
    public SpawnAreaPrefab Prefab { get; set; }
    private Vector2Int startPos;
    public Vector2Int StartPos
    {
        get => startPos;
        set
        {
            startPos = value;
            endPos = new Vector2Int(startPos.x + Prefab.Scale.x - 1, startPos.y + Prefab.Scale.y - 1);
        }
    }
    private Vector2Int endPos;
    public Vector2Int EndPos => endPos;
    private bool isSpawned;
    public void Spawn(Vector2 pos)
    {
        if (!isSpawned)
        {
            Prefab.Spawn(pos);
            isSpawned = true;
        }
    }
}

public class SpawnAreaPrefab : MonoBehaviour
{
    [Header("The array represents a chance of different objects to spawn")]
    [SerializeField] private GameObject[] spawnableObjects;

    [SerializeField] private bool isBig;
    public bool IsBig => isBig;
    [SerializeField] private Vector2Int scale;
    public Vector2Int Scale => scale;

    private float xDiffusion = 0.25f;
    private float yDiffusion = 0.375f;

    public void Spawn(Vector2 pos)
    {
        if(spawnableObjects != null && spawnableObjects.Length != 0)
        {
            GameObject go = Instantiate(spawnableObjects[Random.Range(0, spawnableObjects.Length - 1)]);
            go.transform.position = new Vector2(pos.x + Random.Range(-xDiffusion, xDiffusion), pos.y + Random.Range(-yDiffusion, yDiffusion));
        }
    }
}
