using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Stats;
using Bullet.Command;
using Player.Command;

namespace Player.Effect
{
    public class DoubleShot : ScriptableObject, IPlayerEffect
    {
        public void Execute(GameObject gameObject)
        {
            var player = gameObject.GetComponent<PlayerController>();
            player.ChangeRangedAttack(ScriptableObject.CreateInstance<DualShotRangedAttack>());
        }
        public string GetDescription()
        {
            return "You will now fire two bullets at a time";
        }

        public string GetName()
        {
            return "DoubleShot";
        }
    }
}
