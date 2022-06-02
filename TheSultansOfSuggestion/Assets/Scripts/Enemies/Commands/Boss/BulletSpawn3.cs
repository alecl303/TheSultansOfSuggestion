using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boss.Command
{
    public class BulletSpawn3 : ScriptableObject, IBossCommand
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
                for (int i = 0; i < 360; i += 8)
                {
                    float theta = i * (Mathf.PI / 180);

                    var r = new Vector2(Mathf.Cos(theta), Mathf.Sin(theta)).normalized;

                    var target = r;

                    var x = rigidBody.transform.position.x;
                    var y = rigidBody.transform.position.y;
                    var bulletController = Instantiate(bullet, new Vector3(x + r.x, y + r.y, rigidBody.transform.position.z), Quaternion.Euler(0.0f, 0.0f, i)).gameObject.GetComponent<BossBulletController>();

                    bulletController.SetTarget(target);
                    bulletController.SetBulletDamage(this.bulletDamage);
                    bulletController.SetBulletSpeed(this.speed + r.magnitude);

                    if((i % 32) == 0)
                    {
                        var enemyObject = gameObject.GetComponent<BossController>();
                        var player = enemyObject.GetTarget();

                        Vector2 positionDifference = player.position - rigidBody.position;
                        var angle = Mathf.Atan((positionDifference.y) / (positionDifference.x)) * (180 / Mathf.PI);


                        GameObject bullet2 = (GameObject)Instantiate(enemyObject.targetBulletPrefab, new Vector3(rigidBody.transform.position.x, rigidBody.transform.position.y, rigidBody.transform.position.z), Quaternion.Euler(0.0f, 0.0f, angle));


                        if (positionDifference.x < 0)
                        {
                            bullet2.GetComponent<SpriteRenderer>().flipX = true;
                        }

                        bullet2.GetComponent<BulletController>().SetBulletDamage(enemyObject.GetAttackDamage());
                        bullet2.GetComponent<BulletController>().SetBulletSpeed(8);
                    }

                    boss.StartCoroutine(bufferAttacks(this.attackBuffer));
                }
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
