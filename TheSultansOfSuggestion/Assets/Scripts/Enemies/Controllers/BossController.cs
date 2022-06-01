using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Enemy.Command;

public class BossController : EnemyController
{

    protected override void Init()
    {
        base.Init();

        this.movementSpeed = 5.0f;
        this.attackRange = 2;
        this.health = 300;
        this.aggroDistance = 100;
        this.bulletSpeed = 3;
        this.attackDamage = 2;

        this.chase = ScriptableObject.CreateInstance<BossChase>();
        this.attack = ScriptableObject.CreateInstance<BossRange>();
        //this.rangedAttack = ScriptableObject.CreateInstance<BossRange>();
        //this.attack = ScriptableObject.CreateInstance<MeleeEnemyAttack>();
    }
}
