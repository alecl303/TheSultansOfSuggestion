using UnityEngine;

namespace Player.Command
{
    public interface IPlayerSpell
    {
        void Execute(GameObject gameObject);

        public string GetDescription();
    }
}