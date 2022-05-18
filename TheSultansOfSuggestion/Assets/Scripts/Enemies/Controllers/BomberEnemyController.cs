using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Enemy.Command;

public class BomberEnemyController : EnemyController
{

    private IEnemyCommand chase;

    [SerializeField] public GameObject summonPrefab;
    protected override void Init()
    {
        base.Init();

        this.attackRange = 1;
        this.aggroDistance = 4;
        this.attackDamage = 10;
        this.movementSpeed = 0.5f;
        this.attackBuffer = 5;
        this.health = 3;

        this.chase = ScriptableObject.CreateInstance<EnemyChase>();
        this.attack = ScriptableObject.CreateInstance<SummonerEnemyAttack>();
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