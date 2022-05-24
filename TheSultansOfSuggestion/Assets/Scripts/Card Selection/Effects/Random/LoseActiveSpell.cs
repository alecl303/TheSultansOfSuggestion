using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Stats;
using Bullet.Command;
using Player.Command;

namespace Player.Effect
{
    public class LoseActiveSpell : ScriptableObject, IPlayerEffect
    {
        public void Execute(GameObject gameObject)
        {
            var player = gameObject.GetComponent<PlayerController>();
            player.ChangeSpellAttack(ScriptableObject.CreateInstance<DoNothing>());
        }
        public string GetDescription()
        {
            return "You will lose your active spell";
        }
    }
}
