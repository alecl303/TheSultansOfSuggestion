using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy.Command
{
    public class DoNothing : ScriptableObject, IEnemyCommand
    {
        public void Execute(GameObject gameObject)
        {
            //doing nothing
        }
    }
}