using UnityEngine;

namespace Player.Effect
{
    public interface IPlayerEffect
    {
        void Execute(GameObject gameObject);

        public string GetDescription();

        public string GetName();
    }
}