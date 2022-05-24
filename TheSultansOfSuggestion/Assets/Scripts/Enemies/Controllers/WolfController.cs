using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Enemy.Command;

public class WolfController : EnemyController
{
    protected override void Init()
    {
        base.Init();

        this.movementSpeed = 2;
        this.attackRange = 2;
        this.health = 5;
        this.aggroDistance = 15;

        this.chase = ScriptableObject.CreateInstance<MeleeEnemyDash>();
        this.attack = ScriptableObject.CreateInstance<MeleeEnemyAttack>();
    }
}
