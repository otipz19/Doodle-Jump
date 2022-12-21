using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBonus : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Player.S.ActivateShield();
            Destroy(this.gameObject);
        }
    }
}
