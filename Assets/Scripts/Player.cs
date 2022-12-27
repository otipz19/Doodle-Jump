using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D Rigidbody => rigidbody;
    private new Rigidbody2D rigidbody;

    public float HorizontalSpeed => horizontalSpeed;
    [SerializeField] private float horizontalSpeed = 10f;
    [SerializeField] private float verticalSpeed = 5f;

    [SerializeField] private Shield shield;
    [SerializeField] private SpringBoots springBoots;
    [SerializeField] private Propeller propeller;
    [SerializeField] private Jetpack jetpack;

    public float MaxY => maxY;
    private float maxY;

    static public Player S => s;
    static private Player s;
    private void Awake()
    {
        if (s == null)
            s = this;
        else
            throw new ApplicationException("Player.S is already set");
        rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (maxY - transform.position.y > 50)
            ScoreManager.S.GameOver();
        maxY = transform.position.y > maxY ? transform.position.y : maxY;
        CameraFollow();
    }
    private void CameraFollow()
    {
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z);
    }
    public void FlipBonuses(bool isPlayerFlipped)
    {
        jetpack.FlipX = springBoots.FlipX = !isPlayerFlipped;
    }
    public void GetDamage()
    {
        if (!shield.gameObject.activeSelf)
        {
            GetComponent<Collider2D>().enabled = false;
        }
    }
    public void ActivateBonus(BonusType bonusType)
    {
        switch (bonusType)
        {
            case BonusType.shield:
                shield.gameObject.SetActive(true);
                break;
            case BonusType.springBoots:
                springBoots.gameObject.SetActive(true);
                break;
            case BonusType.propeller:
                propeller.gameObject.SetActive(true);
                break;
            case BonusType.jetpack:
                jetpack.gameObject.SetActive(true);
                break;
        }
    }
    public void Jump(float platformSpeedModifier)
    {
        if (!propeller.gameObject.activeSelf && !jetpack.gameObject.activeSelf)
        {
            Rigidbody.velocity = Vector2.up * verticalSpeed * platformSpeedModifier * (springBoots.gameObject.activeSelf ? springBoots.JumpMod : 1);
            if (springBoots.gameObject.activeSelf)
            {
                springBoots.Animate();
            }
        }
    }
}
