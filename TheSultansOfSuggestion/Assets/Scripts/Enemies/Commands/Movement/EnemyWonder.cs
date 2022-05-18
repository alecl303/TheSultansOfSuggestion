using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.Command
{
    public class EnemyWonder : ScriptableObject, IEnemyCommand
    {
        private float wonderTime = 0;
        private readonly float waitTime = 10;
        private bool wondering = false;
        private Vector2 direction = new Vector2(0f, 0f);
        public void Execute(GameObject gameObject)
        {
            var rigidBody = gameObject.GetComponent<Rigidbody2D>();

            if (rigidBody != null)
            {
                if (this.wondering)
                {
                    rigidBody.velocity = gameObject.GetComponent<EnemyController>().GetSpeed() * this.direction;
                }
            }

            GetAxis(this.direction.x, gameObject);
            WonderTimer();
        }

        private Vector2 RandomDirection()
        {
            float theta = Random.Range(0, 2*Mathf.PI);

            return new Vector2( Mathf.Cos(theta), Mathf.Sin(theta));
        }


        private void GetAxis(float x, GameObject gameObject)
        {
            if(x > 0)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
        }

        public void WonderTimer()
        {
            if (this.wonderTime >= this.waitTime)
            {
                this.wondering = !this.wondering;
                this.direction = RandomDirection();
                this.wonderTime = 0;
            }
            this.wonderTime += Time.deltaTime;
        }
    }
}
