using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Command;

namespace Player.Command
{
    public class SpellNothing : ScriptableObject, IPlayerSpell
    {
        public void Execute(GameObject gameObject)
        {
            // Do nothing
        }

        public string GetDescription()
        {
            return "Do nothing for free.";
        }

        public string GetName()
        {
            return "Nothing";
        }
    }
}
