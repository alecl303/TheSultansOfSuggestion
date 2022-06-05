using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Stats;

namespace Player.Effect
{
    // This is a Test Effect
    public class Take20 : ScriptableObject, IPlayerEffect
    {
        public void Execute(GameObject gameObject)
        {
            gameObject.GetComponent<PlayerStats>().Heal(-20);
        }

        public string GetDescription()
        {
            return "Take 20 hitpoints of damage";
        }

        public string GetName()
        {
            return "Take20";
        }
    }
}

