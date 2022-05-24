using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Stats;
using Bullet.Command;
using Player.Command;

namespace Player.Effect
{
    public class TripleShot : ScriptableObject, IPlayerEffect
    {
        public void Execute(GameObject gameObject)
        {
            var player = gameObject.GetComponent<PlayerController>();
            player.ChangeRangedAttack(ScriptableObject.CreateInstance<TripleShotRangedAttack>());
        }
        public string GetDescription()
        {
            return "You will now fire three bullets at a time";
        }
    }
}