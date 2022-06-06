using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Stats;

namespace Player.Effect
{
    public class StatEffect : ScriptableObject, IPlayerEffect
    {
        protected float changeAmount;
        protected string affectedStat;
        protected string affliction;
        // May not implement
        void OnEnable()
        {
            Init();
        }

        public virtual void Init()
        {
            this.changeAmount = GetRandomAmount();
            this.affliction = "Increase";
        }

        public virtual void Execute(GameObject gameObject)
        {
            //var playerStats = gameObject.GetComponent<PlayerStats>();
        }

        public string GetName()
        {
            return this.name;
        }

        public string GetDescription()
        {
            return (this.affliction + " your " + this.affectedStat + " by " + ((int)(this.changeAmount * 100)) + "%");
        }

        private float GetRandomAmount()
        {
            List<float> randomValues = new List<float>(){0.2f, 0.25f, 0.3f, 0.35f, 0.4f};
            int index = Random.Range(0, 5);
            return randomValues[index];
        }
    }
}