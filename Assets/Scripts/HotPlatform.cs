using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotPlatform : Platform
{
    [SerializeField] private SpriteChanger spriteChanger;
    [SerializeField] private float activationDistance = 5f;
    [SerializeField] private float changeSpriteAcceleration = 1.05f;

    protected override void Update()
    {
        base.Update();
        if (Player.S != null && Vector2.Distance(Player.S.transform.position, transform.position) <= activationDistance)
            StartCoroutine(spriteChanger.DieAnimation(changeSpriteAcceleration));
    }
}
