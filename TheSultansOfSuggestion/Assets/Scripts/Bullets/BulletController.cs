using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : EnemyAttack
{

    private Vector2 target;
    protected float bulletSpeed = 3;

    // Start is called before the first frame update
    void Start()
    {
        this.damage = 2;
        this.target = (FindObjectOfType<PlayerController>().gameObject.GetComponent<Rigidbody2D>().position - this.gameObject.GetComponent<Rigidbody2D>().position).normalized;

    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Rigidbody2D>().velocity = this.bulletSpeed * this.target;
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
        if (collision.gameObject.CompareTag("Environment"))
        {
            Destroy(this.gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("EnemyAttack") || collision.gameObject.CompareTag("Hole"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
}
