using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Enemy.Command;

public class EyeGuyController : EnemyController
{
    protected override void Init()
    {
        base.Init();

        this.attackRange = 6;
        this.aggroDistance = 9;
        this.bulletSpeed = 5;
        this.attackDamage = 10;
        this.movementSpeed = 2;
        this.attackBuffer = 4;

        this.chase = ScriptableObject.CreateInstance<RangedEnemyChase>();
        this.attack = ScriptableObject.CreateInstance<SummonerEnemyAttack>();
    }
}