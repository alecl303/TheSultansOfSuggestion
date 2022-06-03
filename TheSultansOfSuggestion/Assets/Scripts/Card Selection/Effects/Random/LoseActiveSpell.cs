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
            player.SetActiveSpell(ScriptableObject.CreateInstance<SpellNothing>());
        }
        public string GetDescription()
        {
            return "You will lose your active spell";
        }

        public string GetName()
        {
            return "LoseActiveSpell";
        }
    }
}
