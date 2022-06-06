using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Command
{
    public class MeleeAttack : ScriptableObject, IPlayerCommand
    {
        public void Execute(GameObject gameObject)
        {
            var rigidBody = gameObject.GetComponent<Rigidbody2D>();
            var worldTransform = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            var positionDifference = Input.mousePosition - worldTransform;
            var target = positionDifference.normalized;

            GameObject hitBox = (GameObject)Instantiate(gameObject.GetComponent<PlayerController>().hitboxPrefab, new Vector3(rigidBody.transform.position.x + (target.x), rigidBody.transform.position.y + (target.y), rigidBody.transform.position.z), new Quaternion());
            var hitboxController = hitBox.gameObject.GetComponent<PlayerAttack>();

            var playerObject = gameObject.GetComponent<PlayerController>();
            /* Buggy -?
            if(target.x < rigidBody.position.x)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
            */

            hitboxController.SetStunTime(playerObject.GetStats().GetStunTime());
            hitboxController.SetStunChance(playerObject.GetStats().GetStunChance());
            hitboxController.SetPoisonChance(playerObject.GetStats().GetPoisonChance());
            hitboxController.SetPoisonTime(playerObject.GetStats().GetPoisonTime());
            gameObject.GetComponent<PlayerController>().IsAttacking();

            FindObjectOfType<SoundManager>().PlaySoundEffect("Melee2");
        }
    }
}
