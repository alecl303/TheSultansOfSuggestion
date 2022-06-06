using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.Command
{
    public class MeleeEnemyAttack : ScriptableObject, IEnemyCommand
    {
        public void Execute(GameObject gameObject)
        {
            //var rigidBody = gameObject.GetComponent<Rigidbody2D>();
            //var enemyObject = gameObject.GetComponent<EnemyController>();
            //var target = enemyObject.GetTarget();

            // Do nothing for now? Logic handled in meleeController OnCollisionEnter
        }
    }
}