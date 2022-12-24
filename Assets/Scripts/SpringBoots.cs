using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringBoots : MonoBehaviour
{
    public float JumpMod => jumpMod;
    [SerializeField]
    private float jumpMod = 2f;

    [SerializeField]
    private float duration = 5f;
    private float startTime;

    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Animator animator;
    public bool FlipX { set => spriteRenderer.flipX = value; }

    private void OnEnable()
    {
        startTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - startTime >= duration)
        {
            gameObject.SetActive(false);
        }
    }

    public void Animate()
    {
        animator.Play("Jump", 0, 0f);
    }
}
