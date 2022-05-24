using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bullet.Command
{
    public class OrbitBullet : ScriptableObject, IBulletMovement
    {
        private float time = 0;
        private Vector2 target;
        private Vector3 origin;
        private GameObject originObject;
        //private bool originSet = false;
   
        public void Execute(GameObject gameObject)
        {
            this.originObject = gameObject;
            this.origin = FindObjectOfType<PlayerController>().gameObject.GetComponent<Rigidbody2D>().transform.position;
            
            UpdateTarget();
            var bulletController = gameObject.GetComponent<PlayerBulletController>();
            gameObject.GetComponent<Rigidbody2D>().transform.position = this.target;
        }

        private void UpdateTarget()
        {
            this.time += Time.deltaTime;
            var alpha = 10;
            this.target = this.origin + (new Vector3(Mathf.Cos(alpha*this.time), Mathf.Sin(alpha*this.time), 0));
        }
    }
}