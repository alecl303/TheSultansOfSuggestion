using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class PlayerAttack : MonoBehaviour
{
    protected int damage = 2;
    public int GetDamage()
    {
        return this.damage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("PlayerAttack") || collision.gameObject.CompareTag("EnemyAttack"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
        else
        {
            Destroy(this.gameObject);
        }

    }
}
