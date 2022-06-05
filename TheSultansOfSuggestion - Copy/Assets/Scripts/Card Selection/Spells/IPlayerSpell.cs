using UnityEngine;

namespace Player.Command
{
    public interface IPlayerSpell
    {
        void Execute(GameObject gameObject);
        
        public float GetCooldown();
        public string GetDescription();
    }
}