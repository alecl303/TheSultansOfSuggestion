using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Enemy.Command;

public class SummonerEnemyController : EnemyController
{
    protected override void Init()
    {
        base.Init();

        this.attackRange = 8;
        this.aggroDistance = 12;
        this.attackDamage = 1;
        this.movementSpeed = 0.5f;
        this.attackBuffer = 5;
        this.health = 3;

        this.chase = ScriptableObject.CreateInstance<SummonerChase>();
        this.attack = ScriptableObject.CreateInstance<SummonerEnemyAttack>();
    }

}