using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boss.Command
{
    public class DoNothingBoss : ScriptableObject, IBossCommand
    {
        public void Execute(GameObject gameObject)
        {
            //doing nothing
        }

        public float GetDuration()
        {
            return 0;
        }
    }
}