using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Enemy.Command;

public class RangedEnemyController : EnemyController
{

    private IEnemyCommand chase;

    [SerializeField] public GameObject bulletPrefab;
    protected override void Init()
    {
        base.Init();

        base.attackRange = 8;
        base.aggroDistance = 12;
        base.bulletSpeed = 2; 
        base.attackDamage = 1;
        base.movementSpeed = 0.7f;



        this.chase = ScriptableObject.CreateInstance<RangedEnemyChase>();
        this.attack = ScriptableObject.CreateInstance<RangedEnemyAttack>();
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        if (this.IsInChaseRange())
        {
            this.movement = this.chase;
        }

        if ((this.gameObject.GetComponent<Rigidbody2D>().position - this.GetTarget().position).magnitude < this.GetAttackRange())
        {
            this.attack.Execute(this.gameObject);
        }
    }
}