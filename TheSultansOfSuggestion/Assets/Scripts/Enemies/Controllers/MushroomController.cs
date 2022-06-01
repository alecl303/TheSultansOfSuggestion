using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy.Command;

public class MushroomController : EnemyController
{
    // Start is called before the first frame update
    protected override void Init()
    {
        base.Init();

        attackDamage = 5; // We can scale this stuff up as floors progress, so this will have some algorithm to do that here.
        movementSpeed = 5;// 

        // The chase and attack commands are specific to this kind of enemy, so they will be overwritten/declared here.
        this.chase = ScriptableObject.CreateInstance<EnemyChase>();
        this.attack = ScriptableObject.CreateInstance<RangedEnemyAttack>();
    }
}
