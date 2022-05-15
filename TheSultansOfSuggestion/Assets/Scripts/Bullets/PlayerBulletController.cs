using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
    private Vector2 target = new Vector2(0, 0);
    protected float bulletSpeed = 10;
    protected int bulletDamage = 2;


    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Rigidbody2D>().velocity = this.bulletSpeed * this.target;
    }

    public void SetBulletSpeed(float speed)
    {
        this.bulletSpeed = speed;
    }

    public void SetBulletDamage(int bulletDamage)
    {
        this.bulletDamage = bulletDamage;
    }

    public int GetBulletDamage()
    {
        return this.bulletDamage;
    }

    public void SetTarget(Vector2 target)
    {
        this.target = target;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Environment") || collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }
}
