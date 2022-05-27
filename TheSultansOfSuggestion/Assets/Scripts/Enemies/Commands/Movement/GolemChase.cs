using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Enemy.Command;

namespace Enemy.Command
{ 
    public class GolemChase : ScriptableObject, IEnemyCommand
    {
        private bool isDashing = false;
        private Vector2 direction = new Vector2(0, 0);
        private float duration = 1;
        public void Execute(GameObject gameObject)
        {
            var speed = gameObject.GetComponent<EnemyController>().GetSpeed();
            if (!this.isDashing)
            {
                var enemy = gameObject.GetComponent<EnemyController>();

                var playerPosition = FindObjectOfType<PlayerController>().gameObject.GetComponent<Rigidbody2D>().position;
                var enemyPosition = gameObject.GetComponent<Rigidbody2D>().position;
                this.direction = (playerPosition - enemyPosition).normalized;

                enemy.StartCoroutine(Dash(this.duration));
            }

            gameObject.GetComponent<Rigidbody2D>().velocity = this.direction * speed;
        }
        
        public IEnumerator Dash(float duration)
        {
            this.isDashing = true;
            yield return new WaitForSeconds(duration);
            this.isDashing = false;
        }
    }
}

