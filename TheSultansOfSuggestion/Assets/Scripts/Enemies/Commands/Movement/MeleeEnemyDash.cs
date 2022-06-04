using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.Command
{
    public class MeleeEnemyDash : ScriptableObject, IEnemyCommand
    {
        private Vector2 direction;
        private readonly float maxDistance = 3;

        public void Execute(GameObject gameObject)
        {
            var rigidBody = gameObject.GetComponent<Rigidbody2D>();
            var enemy = gameObject.GetComponent<EnemyController>();

            GetDirection(gameObject);

            if (rigidBody != null)
            {
                var positionDifference = (enemy.GetTarget().position - rigidBody.position).magnitude;

                if (positionDifference > this.maxDistance && positionDifference < gameObject.GetComponent<EnemyController>().GetAggroDistance())
                {
                    rigidBody.velocity = enemy.GetSpeed() * this.direction;
                }
                else if (positionDifference < this.maxDistance && !enemy.IsAttacking())
                {
                    enemy.StartCoroutine(enemy.InitiateAttack());
                    enemy.StartCoroutine(MoveToPosition(enemy.GetTarget().transform.position, 0.25f, gameObject));
                    // for(int i = 0; i < 100; i++)
                    // {
                    //     // rigidBody.velocity = enemy.GetSpeed() * 20 * this.direction;
                    // }
                    
                }

            }

            GetAxis(this.direction.x, gameObject);
        }

        private void GetDirection(GameObject gameObject)
        {
            Vector2 deltaPosition = gameObject.GetComponent<EnemyController>().GetTarget().position - gameObject.GetComponent<Rigidbody2D>().position;

            this.direction = deltaPosition.normalized;
        }


        private void GetAxis(float x, GameObject gameObject)
        {
            if (x > 0)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
        
        private IEnumerator MoveToPosition(Vector2 newPosition, float time, GameObject gameObject)
        {
            var timeElapsed  = 0.00f;
            var rigidBody = gameObject.GetComponent<Rigidbody2D>();
            var transform = rigidBody.transform;
            var startPos = rigidBody.transform.position;
            while (timeElapsed < time)
            {
                //Debug.Log(timeElapsed + " out of  " + time);
                rigidBody.MovePosition(Vector2.Lerp(startPos, newPosition, (timeElapsed / time)));
                timeElapsed += Time.deltaTime;
                yield return null;
            }
        }
    }
}
