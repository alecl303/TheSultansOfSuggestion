using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    private Vector2 target;
    protected float bulletSpeed = 3;
    protected int bulletDamage = 2;

    // Start is called before the first frame update
    void Start()
    {
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

    public void SetBulletDamage(int bulletDamage)
    {
        this.bulletDamage = bulletDamage;
    }

    public int GetBulletDamage()
    {
        return this.bulletDamage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Environment") || collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }
}
