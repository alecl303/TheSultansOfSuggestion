using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Stats;

namespace Player.Command
{
    public class MoveCharacterDownRight : ScriptableObject, IPlayerCommand
    {
        public void Execute(GameObject gameObject)
        {
            var rigidBody = gameObject.GetComponent<Rigidbody2D>();
            if (rigidBody != null)
            {
                // Is a 45degree triangle, so just using some trig just do sqrt(2). 
                rigidBody.velocity = new Vector2(gameObject.GetComponent<PlayerStats>().GetSpeed()/Mathf.Sqrt(2), -gameObject.GetComponent<PlayerStats>().GetSpeed()/Mathf.Sqrt(2));
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }
}