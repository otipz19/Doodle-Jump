using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propeller : MonoBehaviour
{
    [SerializeField]
    private GameObject propellerGM;
    [SerializeField]
    private SpriteChanger spriteChanger;
    [SerializeField]
    private float duration = 5f;
    private float startTime;
    [SerializeField]
    private float flySpeed = 10f;

    private void Update()
    {
        if (Time.time - startTime >= duration)
        {
            Player.S.IsFlying = false;
            StopCoroutine(spriteChanger.LoopAnimation());
            propellerGM.SetActive(false);
        }
        if(Player.S.IsFlying)
            Player.S.Rigidbody.velocity = Vector2.up * flySpeed; 
    }

    public void Activate()
    {
        Player.S.IsFlying = true;
        propellerGM.SetActive(true);
        StartCoroutine(spriteChanger.LoopAnimation());
        startTime = Time.time;
    }
}
