using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Command
{
    public class RangedAttack : ScriptableObject, IPlayerCommand
    {
        public void Execute(GameObject gameObject)
        {
            var rigidBody = gameObject.GetComponent<Rigidbody2D>();
            var playerObject = gameObject.GetComponent<PlayerController>();
            //var target = Camera.main.ScreenToWorldPoint(Input.mousePosition).normalized;
    
            GameObject bullet = (GameObject)Instantiate(gameObject.GetComponent<PlayerController>().bulletPrefab, new Vector3(rigidBody.transform.position.x, rigidBody.transform.position.y, rigidBody.transform.position.z), new Quaternion());

            bullet.GetComponent<PlayerBulletController>().SetBulletDamage(playerObject.GetRangeDamage());
            bullet.GetComponent<PlayerBulletController>().SetBulletSpeed(playerObject.GetBulletSpeed());

        }
    }
}


