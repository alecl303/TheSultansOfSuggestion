using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boss.Command
{
    public class RazerSpawn1 : ScriptableObject, IBossCommand
    {
        private Vector2 direction;
        private float radius;
        private float bulletSpeed;
        private bool ready = true;
        private float bulletDamage = 2;
        private float speed = 2;
        private float attackBuffer = 5f;
        private int angleOffset = 0;
        public void Execute(GameObject gameObject)
        {
            var rigidBody = gameObject.GetComponent<Rigidbody2D>();
            var boss = gameObject.GetComponent<BossController>();
            var bullet = boss.razerBulletPrefab;
            var targetBullet = boss.razerBulletPrefab;

            if (rigidBody != null && this.ready)
            {
                var x = rigidBody.transform.position.x;
                var y = rigidBody.transform.position.y;
                var bulletObj = Instantiate(bullet, new Vector3(x, y, rigidBody.transform.position.z), new Quaternion());
                var bulletController = bulletObj.gameObject.GetComponent<RazerBulletController>();
                bulletController.SetBulletDamage(this.bulletDamage);
                bulletController.SetBulletSpeed(this.speed);
            }
        }
        //public IEnumerator bufferAttacks(float attackBuffer)
        //{
        //    this.ready = false;
        //    yield return new WaitForSeconds(attackBuffer);
        //    this.ready = true;
        //}

        public float GetDuration()
        {
            return 3;
        }
    }
}

