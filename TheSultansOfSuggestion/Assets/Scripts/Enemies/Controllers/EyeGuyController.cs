using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Enemy.Command;

public class EyeGuyController : EnemyController
{
    protected override void Init()
    {
        base.Init();

        this.attackRange = 2;
        this.aggroDistance = 3;
        this.bulletSpeed = 2;
        this.attackDamage = 10;
        this.movementSpeed = 0.5f;
        this.attackBuffer = 2;

        this.chase = ScriptableObject.CreateInstance<RangedEnemyChase>();
        this.attack = ScriptableObject.CreateInstance<SummonerEnemyAttack>();
    }
}