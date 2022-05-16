using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHitBoxController : PlayerAttack
{
    private float lifeSpan;
    private float maxDuration;
    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        this.player = FindObjectOfType<PlayerController>();
        this.damage = player.GetMeleeDamage();
        this.lifeSpan = 0;
        this.maxDuration = player.GetFireRate();
    }

    // Update is called once per frame
    void Update()
    {
        this.lifeSpan += Time.deltaTime;
        this.gameObject.GetComponent<Rigidbody2D>().velocity = player.gameObject.GetComponent<Rigidbody2D>().velocity;
        if(this.lifeSpan > this.maxDuration)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!collision.gameObject.CompareTag("Player"))
        Destroy(this.gameObject);
    }


}
