using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boss.Command
{
    public class BulletSpawn4 : ScriptableObject, IBossCommand
    {
        private bool ready = true;
        private float bulletDamage = 2;
        private float speed = 10;
        private float attackBuffer = 0.05f;
        private float i = 0;
        public void Execute(GameObject gameObject)
        {
            var rigidBody = gameObject.GetComponent<Rigidbody2D>();
            var boss = gameObject.GetComponent<BossController>();
            var bullet = boss.bulletPrefab;

            if (rigidBody != null && this.ready)
            {

                float theta = i * (Mathf.PI / 180);

                var target = new Vector2(Mathf.Cos(theta), Mathf.Sin(theta));

                var x = rigidBody.transform.position.x;
                var y = rigidBody.transform.position.y;
                var bulletController = Instantiate(bullet, new Vector3(x, y, rigidBody.transform.position.z), Quaternion.Euler(0.0f, 0.0f, i)).gameObject.GetComponent<BossBulletController>();

                bulletController.SetTarget(target);
                bulletController.SetBulletDamage(this.bulletDamage);
                bulletController.SetBulletSpeed(this.speed);
                i = (i + 13) % 360;

                boss.StartCoroutine(bufferAttacks(this.attackBuffer));

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
            return 10;
        }
    }
}
