using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Effect
{
    // This is a Test Effect
    public class Heal20 : ScriptableObject, IPlayerEffect
    {
        public void Execute(GameObject gameObject)
        {
            gameObject.GetComponent<PlayerController>().GetStats().Heal(20);
        }
    }
}

