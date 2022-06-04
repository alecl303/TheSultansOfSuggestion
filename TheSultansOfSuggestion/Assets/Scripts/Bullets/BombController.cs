using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : EnemyAttack
{
    private int fuseTime = 2;
    void Start()
    {
        this.damage = 10;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;

        StartCoroutine(BombFuse());
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    private IEnumerator BombFuse()
    {
        yield return new WaitForSeconds(this.fuseTime);
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;

        StartCoroutine(BombDisolve());
    }

    private IEnumerator BombDisolve()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(this.gameObject);
    }
}
