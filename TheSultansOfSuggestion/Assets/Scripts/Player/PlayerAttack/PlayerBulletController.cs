using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : PlayerAttack
{
    private Vector2 target = new Vector2(0, 0);
    protected float bulletSpeed = 10;
    private float bulletLifeSpan;
    void Start()
    {
        this.bulletLifeSpan = FindObjectOfType<PlayerController>().GetStats().GetBulletRange();
        StartCoroutine(StartBulletRangeTimer());

        // TODO scale bullet to player 'bullet size' stat
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

    public void SetTarget(Vector2 target)
    {
        this.target = target;
    }

    public IEnumerator StartBulletRangeTimer()
    {
        yield return new WaitForSeconds(this.bulletLifeSpan);

        Destroy(this.gameObject);
    }
}
