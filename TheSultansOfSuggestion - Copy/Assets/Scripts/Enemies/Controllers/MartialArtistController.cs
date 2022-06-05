using System.Collections;
using UnityEngine;

using Enemy.Command;


namespace Assets.Scripts.Enemies.Controllers
{
    public class MartialArtistController : EnemyController
    {
        protected override void Init()
        {
            base.Init();

            this.movementSpeed = 5;
            this.attackRange = 4;
            this.health = 20;
            this.aggroDistance = 15;

            this.chase = ScriptableObject.CreateInstance<MeleeEnemyDash>();
            this.attack = ScriptableObject.CreateInstance<MeleeEnemyAttack>();
        }
    }
}