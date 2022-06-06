using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeBoxController : PlayerAttack
{

    private float fuseTime = 0.2f;
    void Start()
    {
        this.stunTime = 2.0f;
        this.damage = 0;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;

        StartCoroutine(BombFuse());
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    private IEnumerator BombFuse()
    {
        this.gameObject.GetComponent<Transform>().localScale = new Vector3(0.1f, 0.1f, 1);
        yield return new WaitForSeconds(this.fuseTime/8);
        this.gameObject.GetComponent<Transform>().localScale = new Vector3(0.5f, 0.5f, 1);
        yield return new WaitForSeconds(this.fuseTime /8);
        this.gameObject.GetComponent<Transform>().localScale = new Vector3(1.5f, 1.5f, 1);
        yield return new WaitForSeconds(this.fuseTime /8);
        this.gameObject.GetComponent<Transform>().localScale = new Vector3(2.5f, 2.5f, 1);
        yield return new WaitForSeconds(this.fuseTime /8);
        this.gameObject.GetComponent<Transform>().localScale = new Vector3(3, 3, 1);
        yield return new WaitForSeconds(this.fuseTime / 2);
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;

        StartCoroutine(BombDisolve());
    }

    private IEnumerator BombDisolve()
    {
        yield return new WaitForSeconds(this.fuseTime / 8);
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        this.gameObject.GetComponent<Transform>().localScale = new Vector3(3, 3, 1);
        
        this.gameObject.GetComponent<Transform>().localScale = new Vector3(2.5f, 2.5f, 1);
        yield return new WaitForSeconds(this.fuseTime / 8);
        this.gameObject.GetComponent<Transform>().localScale = new Vector3(1.5f, 1.5f, 1);
        yield return new WaitForSeconds(this.fuseTime / 8);
        this.gameObject.GetComponent<Transform>().localScale = new Vector3(0.5f, 0.5f, 1);
        yield return new WaitForSeconds(this.fuseTime / 8);
        this.gameObject.GetComponent<Transform>().localScale = new Vector3(0.1f, 0.1f, 1);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log(collision.gameObject.name);
            collision.gameObject.GetComponent<EnemyController>().InflictStun(this.stunTime);
        }

        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("PlayerAttack"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
}