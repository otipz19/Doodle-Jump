using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    interface ICrossplatformInputController
    {
        Vector2 HorizontalVelocity { get; }
        bool ShootCondition { get; }
    }

    class MobileInputController : ICrossplatformInputController
    {
        public Vector2 HorizontalVelocity => new Vector2(Input.acceleration.x * Player.S.HorizontalSpeed, Player.S.Rigidbody.velocity.y);
        public bool ShootCondition => Input.touches.Length >= 1;
    }

    class DesktopInputController : ICrossplatformInputController
    {
        public Vector2 HorizontalVelocity => new Vector2(Input.GetAxis("Horizontal") * Player.S.HorizontalSpeed, Player.S.Rigidbody.velocity.y);
        public bool ShootCondition => Input.GetAxis("Fire1") > 0;
    }

    private ICrossplatformInputController crossplatformController;

    private float leftTransitionBorder;
    private float rightTransitionBorder;
    [SerializeField] private Sprite rightDirectionSprite;
    [SerializeField] private Sprite forwardDirectionSprite;
    [SerializeField] private float bulletShotDelay = 0.5f;
    private float lastShot;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float spriteChangeDelay = 0.25f;
    [SerializeField] private GameObject nose;
    [SerializeField] private Transform bulletSpawnPoint;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        float cameraHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;
        leftTransitionBorder = -cameraHalfWidth - transform.localScale.x;
        rightTransitionBorder = cameraHalfWidth + transform.localScale.x;
        spriteRenderer = GetComponent<SpriteRenderer>();
        crossplatformController = Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer ?
                                  new MobileInputController() : new DesktopInputController();
    }

    private void Update()
    {
        if (Time.time - lastShot >= bulletShotDelay && crossplatformController.ShootCondition)
        {
            nose.SetActive(true);
            spriteRenderer.sprite = forwardDirectionSprite;
            GameObject bullet = Instantiate<GameObject>(bulletPrefab);
            bullet.transform.position = bulletSpawnPoint.position;
            lastShot = Time.time;
            Invoke("ChangeSpriteAfterShoot", spriteChangeDelay);
        }
    }

    private void ChangeSpriteAfterShoot()
    {
        nose.SetActive(false);
        spriteRenderer.sprite = rightDirectionSprite;
    }

    private void FixedUpdate()
    {
        Player.S.Rigidbody.velocity = crossplatformController.HorizontalVelocity;
        spriteRenderer.flipX = Player.S.Rigidbody.velocity.x < 0;
        Player.S.FlipBonuses(spriteRenderer.flipX);
        if (transform.position.x > rightTransitionBorder)
            transform.position = new Vector2(leftTransitionBorder, transform.position.y);
        else if (transform.position.x < leftTransitionBorder)
            transform.position = new Vector2(rightTransitionBorder, transform.position.y);
    }
}
