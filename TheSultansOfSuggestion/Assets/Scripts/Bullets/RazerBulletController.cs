using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RazerBulletController : EnemyAttack
{

    private Vector2 direction;
    protected float bulletSpeed = 1;

    // Start is called before the first frame update
    void Start()
    {
        this.damage = 5;
        //this.target = (FindObjectOfType<PlayerController>().gameObject.GetComponent<Rigidbody2D>().position - this.gameObject.GetComponent<Rigidbody2D>().position).normalized;
        BounceDirection();
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.GetComponent<Rigidbody2D>().velocity = this.bulletSpeed * this.direction;
    }

    public void SetBulletSpeed(float speed)
    {
        this.bulletSpeed = speed;
    }

    public void SetBulletDamage(float bulletDamage)
    {
        this.damage = bulletDamage;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Environment") || collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Boss") || collision.gameObject.CompareTag("PersistentEnemyAttack") || collision.gameObject.CompareTag("PlayerMeleeAttack"))
        {
            // Destroy(this.gameObject);
            BounceDirection();
        }

        if (collision.gameObject.CompareTag("Boss") || collision.gameObject.CompareTag("EnemyAttack") || collision.gameObject.CompareTag("Hole") || collision.gameObject.CompareTag("PlayerAttack"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Environment"))
        {
            // Destroy(this.gameObject);
            BounceDirection();
        }
    }

    private void BounceDirection() 
    {
        var theta = (Random.Range(0, 360) * Mathf.PI) / 180;
        var radius = 10;
        var newDirection = new Vector2(radius * Mathf.Cos(theta), radius * Mathf.Sin(theta));
        this.direction = newDirection;
    }
}
