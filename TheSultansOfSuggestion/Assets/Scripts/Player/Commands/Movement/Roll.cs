using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Stats;

namespace Player.Command
{
    public class Roll : ScriptableObject, IPlayerCommand
    {
        private Vector2 direction;
        private float duration = 0.5f;
        public void Execute(GameObject gameObject)
        {
            var rigidBody = gameObject.GetComponent<Rigidbody2D>();
            var player = gameObject.GetComponent<PlayerController>();

            if (rigidBody != null)
            {
                float xAxis = Input.GetAxis("Horizontal");
                float yAxis = Input.GetAxis("Vertical");
                this.direction = new Vector2(xAxis, yAxis).normalized;
                rigidBody.velocity = this.direction * player.GetStats().GetSpeed();
                
                player.StartCoroutine(player.Dodge(this.duration, this.direction));
            }
        }
    }
}
