using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class PlayerAttack : MonoBehaviour
{
    protected float damage;
    protected float stunChance;
    protected float stunTime;
    protected float poisonChance;
    protected float poisonTime;
    protected bool canStealHp = false;
    public float GetDamage()
    {
        return this.damage;
    }

    public float GetStunChance()
    {
        return this.stunChance;
    }

    public float GetStunTime()
    {
        return this.stunTime;
    }

    public float GetPoisonChance()
    {
        return this.poisonChance;
    }

    public float GetPoisonTime()
    {
        return this.poisonTime;
    }

    public void SetDamage(float amount)
    {
        this.damage = amount;
    }

    public void SetStunChance(float amount)
    {
        this.stunChance = amount;
    }

    public void SetStunTime(float amount)
    {
        this.stunTime = amount;
    }

    public void SetPoisonChance(float amount)
    {
        this.poisonChance = amount;
    }

    public void SetPoisonTime(float amount)
    {
        this.poisonTime = amount;
    }

    public void SetLifeDrain(bool canSteal)
    {
        this.canStealHp = canSteal;
    }

    public bool CanStealHp()
    {
        return this.canStealHp;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("PlayerAttack") || collision.gameObject.CompareTag("EnemyAttack") || collision.gameObject.CompareTag("Hole"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
