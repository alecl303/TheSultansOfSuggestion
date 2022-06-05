using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Enemy.Command;

public class RangedEnemyController : EnemyController
{

    protected override void Init()
    {
        base.Init();

        this.attackRange = 8;
        this.aggroDistance = 12;
        this.bulletSpeed = 2; 
        this.attackDamage = 1;
        this.movementSpeed = 1.3f;
        this.attackBuffer = 1;

        this.chase = ScriptableObject.CreateInstance<RangedEnemyChase>();
        this.attack = ScriptableObject.CreateInstance<RangedEnemyAttack>();
    }

}