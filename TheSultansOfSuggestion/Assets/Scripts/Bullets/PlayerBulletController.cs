using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
    private Vector2 target;
    protected float bulletSpeed = 1;
    protected int bulletDamage = 2;

    // Start is called before the first frame update
    void Start()
    {
        //var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //this.target = (new Vector2(mousePosition.x, mousePosition.y) - this.gameObject.GetComponent<Rigidbody2D>().position).normalized;

        var worldTransform = Camera.main.WorldToScreenPoint(transform.position);
        var positionDifference = Input.mousePosition - worldTransform;

        this.target = positionDifference.normalized;
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
        if (collision.gameObject.CompareTag("Environment"))
        {
            Destroy(this.gameObject);
        }
    }
}
