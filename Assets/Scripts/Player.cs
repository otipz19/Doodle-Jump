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

    public bool IsShieldActive { get; set; }
    [SerializeField]
    private Shield shield;

    public bool IsSpringBootsActive { get; set; }
    [SerializeField]
    private SpringBoots springBoots;

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
        springBoots.FlipX = !spriteRenderer.flipX;
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
        if (!IsShieldActive)
            Destroy(this.gameObject);
    }

    public void ActivateShield()
    {
        shield.Activate();
    }

    public void ActivateSpringBoots()
    {
        springBoots.Activate();
    }

    public void Jump(float platformSpeedModifier)
    {
        Rigidbody.velocity = Vector2.up * verticalSpeed * platformSpeedModifier * (IsSpringBootsActive ? springBoots.JumpMod : 1);
        StopCoroutine(springBoots.Animate());
        StartCoroutine(springBoots.Animate());
    }
}
