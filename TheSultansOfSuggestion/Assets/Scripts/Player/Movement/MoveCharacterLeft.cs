using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Command;

namespace Player.Command
{
    public class MoveCharacterLeft : ScriptableObject, IPlayerCommand
    {
        public void Execute(GameObject gameObject)
        {
            var rigidBody = gameObject.GetComponent<Rigidbody2D>();
            if (rigidBody != null)
            {
                rigidBody.velocity = new Vector2(-gameObject.GetComponent<PlayerController>().GetSpeed(), rigidBody.velocity.y);
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }
}
