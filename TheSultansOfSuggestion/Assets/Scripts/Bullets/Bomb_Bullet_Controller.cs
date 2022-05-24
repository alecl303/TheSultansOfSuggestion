using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_Bullet_Controller : BombController
{
    private float bulletSpeed = 1;
    void Update()
    {
        var target = (FindObjectOfType<PlayerController>().gameObject.GetComponent<Rigidbody2D>().position - this.gameObject.GetComponent<Rigidbody2D>().position).normalized;
        this.GetComponent<Rigidbody2D>().velocity = this.bulletSpeed * target;
    }
}
