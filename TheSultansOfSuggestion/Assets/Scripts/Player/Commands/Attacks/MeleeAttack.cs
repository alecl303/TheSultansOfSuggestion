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
            var theta = Mathf.Atan((positionDifference.y - rigidBody.transform.position.y) / (positionDifference.x - rigidBody.transform.position.x)) * (180 / Mathf.PI);
            GameObject hitBox = (GameObject)Instantiate(gameObject.GetComponent<PlayerController>().hitboxPrefab, new Vector3(rigidBody.transform.position.x + (target.x), rigidBody.transform.position.y + (target.y), rigidBody.transform.position.z), Quaternion.Euler(0.0f, 0.0f, theta));
            var hitboxController = hitBox.gameObject.GetComponent<PlayerAttack>();
            //hitBox.GetComponentInChildren<Transform>().Rotate(new Vector3(0.0f, 0.0f, theta/ ((180 / Mathf.PI))));
            if (positionDifference.x - rigidBody.transform.position.x < 0)
            {
                hitBox.GetComponentInChildren<SpriteRenderer>().flipX = true;
                hitBox.GetComponentInChildren<SpriteRenderer>().flipY = true;
            }
            var playerObject = gameObject.GetComponent<PlayerController>();
            

            hitboxController.SetStunTime(playerObject.GetStats().GetStunTime());
            hitboxController.SetStunChance(playerObject.GetStats().GetStunChance());
            hitboxController.SetPoisonChance(playerObject.GetStats().GetPoisonChance());
            hitboxController.SetPoisonTime(playerObject.GetStats().GetPoisonTime());
            gameObject.GetComponent<PlayerController>().IsAttacking();

            FindObjectOfType<SoundManager>().PlaySoundEffect("Melee2");
        }
    }
}
