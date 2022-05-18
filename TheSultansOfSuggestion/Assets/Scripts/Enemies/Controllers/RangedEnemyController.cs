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

        this.attackRange = 8;
        this.aggroDistance = 12;
        this.bulletSpeed = 2; 
        this.attackDamage = 1;
        this.movementSpeed = 1.3f;

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

        if ((this.gameObject.GetComponent<Rigidbody2D>().position - this.GetTarget().position).magnitude < this.GetAttackRange() && !this.IsAttacking())
        {
            this.attack.Execute(this.gameObject);
            StartCoroutine(InitiateAttack());
        }
    }
}