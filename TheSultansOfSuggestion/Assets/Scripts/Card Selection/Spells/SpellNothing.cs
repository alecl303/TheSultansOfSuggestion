using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Command;

namespace Player.Command
{
    public class SpellNothing : ScriptableObject, IPlayerSpell
    {

        private int requiredMana = 0;

        public void Execute(GameObject gameObject)
        {
            // Do nothing
        }

        public string GetDescription()
        {
            return "Do nothing for free.";
        }
    }
}
