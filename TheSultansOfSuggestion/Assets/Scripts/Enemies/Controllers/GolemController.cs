using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Enemy.Command;

public class GolemController : EnemyController
{

    protected override void Init()
    {
        base.Init();

        this.attackRange = 5;
        this.aggroDistance = 15;
        this.attackDamage = 4;
        this.movementSpeed = 4;
        this.attackBuffer = 1;

        this.chase = ScriptableObject.CreateInstance<GolemChase>();
        this.attack = ScriptableObject.CreateInstance<GolemRangedAttack>();
    }

}