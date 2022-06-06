
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.Command;

namespace Player.Command
{
    public class DoNothing : ScriptableObject, IPlayerCommand
    {
        public void Execute(GameObject gameObject)
        {
            //doing nothing
        }
    }
}