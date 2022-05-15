using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Command
{
    public class MoveCharacterDown : ScriptableObject, IPlayerCommand
    {
        public void Execute(GameObject gameObject)
        {
            var rigidBody = gameObject.GetComponent<Rigidbody2D>();
            if (rigidBody != null)
            {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, -gameObject.GetComponent<PlayerController>().GetSpeed());
            }
        }
    }
}