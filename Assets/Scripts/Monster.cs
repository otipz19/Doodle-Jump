using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    protected Mover mover;
    protected float moveRange;
    protected SpriteChanger spriteChanger;
    [SerializeField]
    private int hitpoints = 1;

    protected abstract void Initialize();

    private void Start()
    {
        mover = GetComponent<Mover>();
        TryGetComponent<SpriteChanger>(out spriteChanger);
        Initialize();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Player":
                Player.S.GetDamage();
                break;
            case "PlayerBullet":
                if (--hitpoints <= 0)
                {
                    Destroy(this.gameObject);
                    Destroy(other.gameObject);
                }
                break;
        }
    }
}
