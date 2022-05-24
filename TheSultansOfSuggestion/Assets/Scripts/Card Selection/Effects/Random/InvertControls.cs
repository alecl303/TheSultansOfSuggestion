using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Stats;
using Bullet.Command;
using Player.Command;

namespace Player.Effect
{
    public class InvertControls : ScriptableObject, IPlayerEffect
    {
        public void Execute(GameObject gameObject)
        {
            var player = gameObject.GetComponent<PlayerController>();
            player.InvertControls();
        }
        public string GetDescription()
        {
            return "Movement controls are inverted";
        }
    }
}
