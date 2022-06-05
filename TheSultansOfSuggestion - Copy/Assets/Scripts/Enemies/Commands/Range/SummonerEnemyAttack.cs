using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.Command
{
    public class SummonerEnemyAttack : ScriptableObject, IEnemyCommand
    {
        public void Execute(GameObject gameObject)
        {
            var rigidBody = gameObject.GetComponent<Rigidbody2D>();
            var enemyObject = gameObject.GetComponent<EnemyController>();
            var target = enemyObject.GetTarget();

            Vector2 positionDifference = target.position - rigidBody.position;

            if (positionDifference.magnitude < enemyObject.GetAttackRange())
            {
                GameObject wolf = (GameObject)Instantiate(gameObject.GetComponent<EnemyController>().bulletPrefab, new Vector3(rigidBody.transform.position.x + (positionDifference.normalized.x / 3), rigidBody.transform.position.y + (positionDifference.normalized.y / 3), rigidBody.transform.position.z), new Quaternion());
                enemyObject.StartCoroutine(enemyObject.InitiateAttack());
            }
        }
    }
}