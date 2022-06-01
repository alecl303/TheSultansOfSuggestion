using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Enemy.Command;

namespace Enemy.Command
{
    public class BossRange: ScriptableObject, IEnemyCommand
    {
        public void Execute(GameObject gameObject)
        {
            var rigidBody = gameObject.GetComponent<Rigidbody2D>();
            var enemyObject = gameObject.GetComponent<EnemyController>();
            var target = enemyObject.GetTarget();

            Vector2 positionDifference = target.position - rigidBody.position;

            if (positionDifference.magnitude < enemyObject.GetAttackRange())
            {
                var theta = Mathf.Atan((positionDifference.y) / (positionDifference.x)) * (180 / Mathf.PI);


                GameObject bullet = (GameObject)Instantiate(enemyObject.bulletPrefab, new Vector3(rigidBody.transform.position.x, rigidBody.transform.position.y, rigidBody.transform.position.z), Quaternion.Euler(0.0f, 0.0f, theta));


                if (positionDifference.x < 0)
                {
                    bullet.GetComponent<SpriteRenderer>().flipX = true;
                }

                bullet.GetComponent<BulletController>().SetBulletDamage(enemyObject.GetAttackDamage());
                bullet.GetComponent<BulletController>().SetBulletSpeed(enemyObject.GetBulletSpeed());

                enemyObject.StartCoroutine(enemyObject.InitiateAttack());
            }
        }
    }
}
