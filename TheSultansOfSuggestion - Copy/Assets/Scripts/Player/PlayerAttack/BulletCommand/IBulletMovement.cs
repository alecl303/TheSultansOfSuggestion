using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bullet.Command
{
    public interface IBulletMovement
    {
        public void Execute(GameObject gameObject);
    }
}