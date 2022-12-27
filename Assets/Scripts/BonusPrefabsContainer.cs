using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPrefabsContainer : MonoBehaviour
{
    [SerializeField] public GameObject[] BonusPrefabs;
    private static BonusPrefabsContainer s;
    public static BonusPrefabsContainer S => s;
    private void Awake()
    {
        if (s != null)
            throw new System.ApplicationException("BonusPrefabsContainer.S is already asigned");
        s = this;
    }
}
