using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Stats;

namespace Player.Command
{
    public class MoveCharacterRight : ScriptableObject, IPlayerCommand
    {
        public void Execute(GameObject gameObject)
        {
            var rigidBody = gameObject.GetComponent<Rigidbody2D>();
            if (rigidBody != null)
            {
                rigidBody.velocity = new Vector2(gameObject.GetComponent<PlayerStats>().GetSpeed(), rigidBody.velocity.y);
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }
}