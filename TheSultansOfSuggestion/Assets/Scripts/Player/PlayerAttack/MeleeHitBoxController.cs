using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHitBoxController : PlayerAttack
{
    private float lifeSpan;
    private float maxDuration;
    private PlayerController player;

    [SerializeField] private bool showHitBox = false;

    // Start is called before the first frame update
    void Start()
    {
        this.player = FindObjectOfType<PlayerController>();
        this.damage = player.GetStats().GetMeleeDamage();
        this.lifeSpan = 0;
        this.maxDuration = player.GetStats().GetFireRate();
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

        if (this.showHitBox)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(100, 10, 5, 10);
        }
    }

}
