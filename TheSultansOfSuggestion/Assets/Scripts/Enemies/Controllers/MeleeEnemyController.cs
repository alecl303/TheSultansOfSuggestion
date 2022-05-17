using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Enemy.Command;

public class MeleeEnemyController : EnemyController
{
    // Melee enemies will all have a 'chase' command, so that is declared here.
    private IEnemyCommand chase;

    // Override Init() will overwrite the parent class EnemyController's Init.
    protected override void Init()
    {
        // base.Init() will execute parent class EnemyController's Init(), thus inheriting it's functionality
        base.Init();

        // V------------------    Any additional 'start' functionality will go down here    ---------------------V

        base.attackDamage = 2; // We can scale this stuff up as floors progress, so this will have some algorithm to do that here.
        base.movementSpeed = 1;// 

        // The chase and attack commands are specific to this kind of enemy, so they will be overwritten/declared here.
        this.chase = ScriptableObject.CreateInstance<EnemyChase>();
        this.attack = ScriptableObject.CreateInstance<MeleeEnemyAttack>();
    }


    // Override OnUpdate() will overwrite the parent class EnemyController's OnUpdate().
    protected override void OnUpdate()
    {
        // base.OnUpdate() will execute parent class EnemyController's OnUpdate(), thus inheriting it's functionality
        base.OnUpdate();


        // Any additional 'update' functionality specific to this type of enemy will go here.
        if (this.IsInChaseRange())
        {
            this.movement = this.chase;
        }

    }

    // V-------------------------------------- Any helper functions specific to this type of enemy would go down here --------------------------V


    // See RangeEnemyController for cleaner, non commented execution of this pattern.
}
