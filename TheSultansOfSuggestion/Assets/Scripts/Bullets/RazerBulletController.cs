using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RazerBulletController : EnemyAttack
{

    private Vector2 direction;
    protected float bulletSpeed = 9;

    // Start is called before the first frame update
    void Start()
    {
        this.damage = 2;
        //this.target = (FindObjectOfType<PlayerController>().gameObject.GetComponent<Rigidbody2D>().position - this.gameObject.GetComponent<Rigidbody2D>().position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Rigidbody2D>().velocity = this.bulletSpeed * this.direction;
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
            // Destroy(this.gameObject);
            bounceDirection();
            Debug.Log("bounce");

        }

        if (collision.gameObject.CompareTag("Boss") || collision.gameObject.CompareTag("EnemyAttack") || collision.gameObject.CompareTag("Hole") || collision.gameObject.CompareTag("PlayerAttack"))
        {
            Debug.Log("ignore");
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
    
    private void bounceDirection() 
    {
        var newDirection = -direction;
        this.direction = newDirection;
    }
}
