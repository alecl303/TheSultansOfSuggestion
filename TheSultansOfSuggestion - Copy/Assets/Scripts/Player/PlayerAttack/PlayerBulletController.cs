using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Bullet.Command;

public class PlayerBulletController : PlayerAttack
{
    private Vector2 target = new Vector2(0, 0);
    protected float bulletSpeed = 10;
    private float bulletLifeSpan;

    private IBulletMovement movement;
    void Start()
    {
        this.bulletLifeSpan = FindObjectOfType<PlayerController>().GetStats().GetBulletRange();
        StartCoroutine(StartBulletRangeTimer());

        this.movement = FindObjectOfType<PlayerController>().GetStats().GetBulletMovement();
        // TODO scale bullet to player 'bullet size' stat
    }

    // Update is called once per frame
    void Update()
    {
        this.movement.Execute(this.gameObject);
    }

    public void SetBulletSpeed(float speed)
    {
        this.bulletSpeed = speed;
    }

    public void SetTarget(Vector2 target)
    {
        this.target = target;
    }

    public float GetBulletSpeed()
    {
        return this.bulletSpeed;
    }

    public Vector2 GetTarget()
    {
        return this.target;
    }

    public IEnumerator StartBulletRangeTimer()
    {
        yield return new WaitForSeconds(this.bulletLifeSpan);

        Destroy(this.gameObject);
    }
}
