using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jetpack : Propeller
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    public bool FlipX
    {
        set
        {
            spriteRenderer.flipX = value;
            transform.localPosition = value ? new Vector2(-0.36f, transform.localPosition.y) :
                                              new Vector2(0.36f, transform.localPosition.y);
        }
    }

    protected override void Update()
    {
        base.Update();
        float timePassed = Time.time - startTime;
        if(timePassed >= 9.3f)
            animator.SetBool("Jetpack_End", true);    
    }
}
