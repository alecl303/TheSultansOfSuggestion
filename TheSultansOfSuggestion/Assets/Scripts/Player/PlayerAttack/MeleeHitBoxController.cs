using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHitBoxController : PlayerAttack
{
    private float lifeSpan;
    private float maxDuration;

    // Start is called before the first frame update
    void Start()
    {
        this.damage = FindObjectOfType<PlayerController>().GetMeleeDamage();
    }

    // Update is called once per frame
    void Update()
    {
        this.lifeSpan += Time.deltaTime;

        if(this.lifeSpan > this.maxDuration)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);
    }


}
