using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Stats;

namespace Player.Effect
{
    public class DropTo1HP : ScriptableObject, IPlayerEffect
    {
        public void Execute(GameObject gameObject)
        {
            gameObject.GetComponent<PlayerStats>().health = 1;
        }

        public string GetDescription()
        {
            return "Hp drops to 1";
        }

        public string GetName()
        {
            return "DropTo1HP";
        }
    }
}

