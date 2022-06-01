using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using Enemy.Command;

namespace Enemy.Command
{
    public class BossChase : ScriptableObject, IEnemyCommand
    {
        // >50% hp: bullet pattern, pause, aoe
        // <50% hp: bullet pattern, pause, dash + aoe x3
        private bool enrage = false;
        private string state = "idle";
        private bool firePattern = false;

        private Vector2 direction = new Vector2(0, 0);
        private bool smash = false;
        private bool dashing = false;
        private float shortCooldown = 2;
        private float longCooldown = 5;
        public void Execute(GameObject gameObject)
        {
            var speed = gameObject.GetComponent<EnemyController>().GetSpeed();
            var enemy = gameObject.GetComponent<EnemyController>();
            var rigidBody = gameObject.GetComponent<Rigidbody2D>();
            var bullet = enemy.bulletPrefab;
            if (this.state == "idle")
            {
                enemy.StartCoroutine(FirePattern(longCooldown));
            }
            else if (this.state == "firePattern")
            {
                this.state = "idle";
                /* for (int i = 0; i < 8; i ++)
                {
                    float theta = i * 360 * (Mathf.PI / 180);

                    var r = Mathf.Sin(1.3f * i);

                    var target = new Vector2(enemy.transform.position.x + (r * Mathf.Cos(theta)), enemy.transform.position.y + (r * Mathf.Sin(theta)));

                    var x = target.x; // + target.x;
                    var y = target.y; // + target.y;
                    var bulletController = Instantiate(bullet, new Vector3(x, y, enemy.transform.position.z), Quaternion.Euler(0.0f, 0.0f, i)).gameObject.GetComponent<BulletController>();

                    // bulletController.SetBulletTarget(1.1f * target);
                    bulletController.SetBulletDamage(enemy.GetAttackDamage());
                    bulletController.SetBulletSpeed(enemy.GetBulletSpeed());
                }*/
            }
            else if (this.state == "dashing")
            {
                
            }
            else if (this.state == "smashing")
            {
                // AOE
            }
            // gameObject.GetComponent<Rigidbody2D>().velocity = this.direction * speed;
        }

        // once it stops firing, it starts a dash
        public IEnumerator FirePattern(float duration)
        {
            this.state = "firePattern";
            yield return new WaitForSeconds(duration);
            this.state = "idle";
            if (this.enrage)
            {
                this.state = "dashing";
            }
        }

        // once it stops dashing, it starts firing
        public IEnumerator Dash(float duration)
        {
            this.dashing = true;
            yield return new WaitForSeconds(duration);
            // attack here
            this.dashing = false;
            this.firePattern = true;
        }


    }
}
