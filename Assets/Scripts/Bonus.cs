using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BonusType
{
    shield,
    springBoots,
    propeller,
    jetpack,
}

public class Bonus : MonoBehaviour
{
    [SerializeField]
    private BonusType bonusType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player.S.ActivateBonus(bonusType);
            Destroy(this.gameObject);
        }
    }
}
