using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boss.Command
{
    public interface IBossCommand
    {
        void Execute(GameObject gameObject);
        float GetDuration();
    }
}