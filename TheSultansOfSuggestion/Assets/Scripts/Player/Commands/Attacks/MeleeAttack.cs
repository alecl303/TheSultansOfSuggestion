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
            var target = new Vector2(0, 0);
            var positionDifference = new Vector2(0, 0);

            if (Input.GetJoystickNames()[0].Equals(""))
            {
                var worldTransform = Camera.main.WorldToScreenPoint(gameObject.transform.position);
                positionDifference = Input.mousePosition - worldTransform;
                target = positionDifference.normalized;

            }
            else
            {
                target = new Vector2(-Input.GetAxis("4"), -Input.GetAxis("5")).normalized;

                if (target.magnitude == 0)
                {
                    var xValue = 1;
                    if (gameObject.GetComponent<SpriteRenderer>().flipX)
                    {
                        xValue = -1;
                    }
                    target = new Vector2(xValue, 0);
                }

                positionDifference = rigidBody.position + target;
            }

            var theta = Mathf.Atan((positionDifference.y - rigidBody.transform.position.y) / (positionDifference.x - rigidBody.transform.position.x)) * (180 / Mathf.PI);


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
