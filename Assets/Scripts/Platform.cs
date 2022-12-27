using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private Transform bonusSpawnPos;
    [SerializeField] private float bonusSpawnChance = 0.1f;
    private GameObject bonus;

    protected virtual void Start()
    {
        if(Random.value < bonusSpawnChance)
            bonus = Instantiate(BonusPrefabsContainer.S.BonusPrefabs[Random.Range(0, BonusPrefabsContainer.S.BonusPrefabs.Length - 1)]);
    }

    protected virtual void Update()
    {
        if(bonus != null)
            bonus.transform.position = bonusSpawnPos.position;
    }
}
