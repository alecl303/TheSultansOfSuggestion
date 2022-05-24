using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.Command
{
    public class RangedEnemyChase : ScriptableObject, IEnemyCommand
    {
        private Vector2 direction;
        public readonly float maxDistance = 2;

        public void Execute(GameObject gameObject)
        {
            var rigidBody = gameObject.GetComponent<Rigidbody2D>();

            GetDirection(gameObject);

            if (rigidBody != null)
            {
                var positionDifference = (gameObject.GetComponent<EnemyController>().GetTarget().position - rigidBody.position).magnitude;

                if (positionDifference > maxDistance && positionDifference < gameObject.GetComponent<EnemyController>().GetAggroDistance())
                {
                    rigidBody.velocity = gameObject.GetComponent<EnemyController>().GetSpeed() * this.direction;
                }else if(positionDifference < maxDistance)
                {
                    rigidBody.velocity = gameObject.GetComponent<EnemyController>().GetSpeed() * 0.5f * -this.direction;
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
    }
}
