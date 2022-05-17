using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : PlayerAttack
{
    private Vector2 target = new Vector2(0, 0);
    protected float bulletSpeed = 10;

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
        this.damage = bulletDamage;
    }

    public void SetTarget(Vector2 target)
    {
        this.target = target;
    }
}
