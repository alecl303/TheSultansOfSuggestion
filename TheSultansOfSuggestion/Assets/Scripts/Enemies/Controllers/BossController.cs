using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Enemy.Command;

public class BossController : EnemyController
{
    protected override void Init()
    {
        base.Init();

        this.movementSpeed = 20;
        this.attackRange = 2;
        this.health = 100;
        this.aggroDistance = 100;

        this.chase = ScriptableObject.CreateInstance<MeleeEnemyDash>();
        this.attack = ScriptableObject.CreateInstance<MeleeEnemyAttack>();
    }
}
