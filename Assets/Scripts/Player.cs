using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    interface ICrossplatformPlayerController
    {
        Vector2 HorizontalVelocity { get; }
        bool ShootCondition { get; }
    }

    class MobilePlayerController : ICrossplatformPlayerController
    {
        public Vector2 HorizontalVelocity => new Vector2(Input.acceleration.x * Player.S.horizontalSpeed, Player.S.Rigidbody.velocity.y);
        public bool ShootCondition => Input.touches.Length >= 1;
    }

    class DesktopPlayerController : ICrossplatformPlayerController
    {
        public Vector2 HorizontalVelocity => new Vector2(Input.GetAxis("Horizontal") * Player.S.horizontalSpeed, Player.S.Rigidbody.velocity.y);
        public bool ShootCondition => Input.GetAxis("Fire1") > 0;
    }

    static public Player S => s;

    static private Player s;

    private ICrossplatformPlayerController crossplatformController;

    [SerializeField]
    private Sprite rightDirectionSprite;
    [SerializeField]
    private Sprite forwardDirectionSprite;
    [SerializeField]
    private GameObject nose;
    [SerializeField]
    private Transform bulletSpawnPoint;
    private SpriteRenderer spriteRenderer;

    public Rigidbody2D Rigidbody => rigidbody;
    private new Rigidbody2D rigidbody;

    [SerializeField]
    private float horizontalSpeed = 10f;
    [SerializeField]
    private float verticalSpeed = 5f;

    private float leftTransitionBorder;
    private float rightTransitionBorder;

    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float spriteChangeDelay = 0.25f;
    [SerializeField]
    private float bulletShotDelay = 0.5f;
    private float lastShot;

    [SerializeField]
    private Shield shield;
    [SerializeField]
    private SpringBoots springBoots;
    [SerializeField]
    private Propeller propeller;
    [SerializeField]
    private Jetpack jetpack;

    public float MaxY => maxY;
    private float maxY;

    private void Awake()
    {
        if (s == null)
            s = this;
        else
            throw new ApplicationException("Player.S is already set");

        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        crossplatformController = Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer ?
                                  new MobilePlayerController() : new DesktopPlayerController();
    }

    private void Start()
    {
        float cameraHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;
        leftTransitionBorder = -cameraHalfWidth - transform.localScale.x;
        rightTransitionBorder = cameraHalfWidth + transform.localScale.x;
    }

    private void Update()
    {
        if (Time.time - lastShot >= bulletShotDelay && crossplatformController.ShootCondition)
        {
            Shoot();
        }
        if (maxY - transform.position.y > 50)
            ScoreManager.S.GameOver();
        maxY = transform.position.y > maxY ? transform.position.y : maxY;
    }

    private void FixedUpdate()
    {
        MoveHorizontaly();
        CameraFollow();
        CheckTransitionBorder();
    }

    private void CheckTransitionBorder()
    {
        if (transform.position.x > rightTransitionBorder)
        {
            transform.position = new Vector2(leftTransitionBorder, transform.position.y);
        }
        else if (transform.position.x < leftTransitionBorder)
        {
            transform.position = new Vector2(rightTransitionBorder, transform.position.y);
        }
    }

    private void MoveHorizontaly()
    {
        Rigidbody.velocity = crossplatformController.HorizontalVelocity;
        spriteRenderer.flipX = Rigidbody.velocity.x < 0;
        jetpack.FlipX = springBoots.FlipX = !spriteRenderer.flipX;
    }

    private void CameraFollow()
    {
        Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z);
    }

    private void Shoot()
    {
        nose.SetActive(true);
        spriteRenderer.sprite = forwardDirectionSprite;
        GameObject bullet = Instantiate<GameObject>(bulletPrefab);
        bullet.transform.position = bulletSpawnPoint.position;
        lastShot = Time.time;
        Invoke("ChangeSpriteAfterShoot", spriteChangeDelay);
    }

    private void ChangeSpriteAfterShoot()
    {
        nose.SetActive(false);
        spriteRenderer.sprite = rightDirectionSprite;
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
