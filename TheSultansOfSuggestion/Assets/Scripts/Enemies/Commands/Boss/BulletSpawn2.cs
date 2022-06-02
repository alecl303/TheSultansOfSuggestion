using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boss.Command
{
    public class BulletSpawn2 : ScriptableObject, IBossCommand
    {
        private Vector2 direction;
        private float radius;
        private float bulletSpeed;
        private bool ready = true;
        private float bulletDamage = 2;
        private float speed = 2;
        private float attackBuffer = 1f;
        private int angleOffset = 0;
        public void Execute(GameObject gameObject)
        {
            var rigidBody = gameObject.GetComponent<Rigidbody2D>();
            var boss = gameObject.GetComponent<BossController>();
            var bullet = boss.bulletPrefab;
            var targetBullet = boss.targetBulletPrefab;

            if (rigidBody != null && this.ready)
            {
                for (int i = angleOffset ; i < 720 + angleOffset; i += 15)
                {
                    float theta = i * (Mathf.PI / 180);

                    var r = Mathf.Sin(1.4f * theta);

                    var target = new Vector2((Mathf.Max(r, 0.01f) * Mathf.Cos(theta)), (Mathf.Max(r, 0.01f) * Mathf.Sin(theta))).normalized;

                    var x = rigidBody.transform.position.x;
                    var y = rigidBody.transform.position.y;
                    var bulletController = Instantiate(bullet, new Vector3(x, y, rigidBody.transform.position.z), Quaternion.Euler(0.0f, 0.0f, i)).gameObject.GetComponent<BossBulletController>();

                    bulletController.SetTarget(target);
                    bulletController.SetBulletDamage(this.bulletDamage);
                    bulletController.SetBulletSpeed(this.speed);

                    boss.StartCoroutine(bufferAttacks(this.attackBuffer));
                }

                this.angleOffset += 30;
            }
        }

        public IEnumerator bufferAttacks(float attackBuffer)
        {
            this.ready = false;
            yield return new WaitForSeconds(attackBuffer);
            this.ready = true;
        }

        public float GetDuration()
        {
            return 3;
        }
    }
}