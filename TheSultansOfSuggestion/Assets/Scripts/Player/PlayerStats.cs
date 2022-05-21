using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Bullet.Command;

namespace Player.Stats
{
    public class PlayerStats : MonoBehaviour
    {
        public float movementSpeed = 2.0f;
        public float health = 100;
        public float maxHealth = 100;
        public float mana = 100;
        public float maxMana = 100;
        public float bulletSpeed = 3;
        public float bulletLifeSpan = 0.5f;
        public float stunTime = 1;
        public float stunChance = 0;
        public float lifeSteal = 0;
        public float poisonChance = 0;
        public float poisonTime = 3;
        public float poisonTickDamage = 1;
        //public float bulletSize = 1; If we use bulletSize+ effect (YAGNI?)
        public float rangeDamage = 2;
        public float meleeDamage = 5;
        public float fireRate = 0.4f;
        public int critChance = 1;
        public List<GameObject> weapons;
        public GameObject activeWeapon;
        public IBulletMovement bulletMovement;

        public int weaponDamage;

        //private PlayerController playerController;
        // Start is called before the first frame update
        void Start()
        {
            //this.playerController = this.gameObject.GetComponent<PlayerController>();
            this.weapons.Add(this.activeWeapon);

            this.weaponDamage = this.activeWeapon.GetComponent<Weapon>().GetDamage();

            this.bulletMovement = ScriptableObject.CreateInstance<StandardBullet>();
        }

        public float GetSpeed()
        {
            return this.movementSpeed;
        }

        public float GetBulletSpeed()
        {
            return this.bulletSpeed;
        }

        public float GetBulletRange()
        {
            return this.bulletLifeSpan;
        }

        public float GetRangeDamage()
        {
            return this.rangeDamage;
        }

        public float GetMeleeDamage()
        {
            this.weaponDamage = this.activeWeapon.GetComponent<Weapon>().GetDamage();

            var damage = this.meleeDamage + this.weaponDamage;

            if (Random.Range(0, 100) <= this.critChance)
            {
                Debug.Log("Crit!"); // TODO: Insert UI logic
                return damage * 2;
            }
            return damage;  // Will have to figure out active weapon in inventory
        }

        public void Heal(float amount)
        {
            this.health += Mathf.Min(amount, this.maxHealth - this.health);
        }

        public float GetMana()
        {
            return this.mana;
        }

        public void DrainMana(float amount)
        {
            this.mana -= amount;
        }

        public float GetFireRate()
        {
            return this.fireRate;
        }

        public float GetStunTime()
        {
            return this.stunTime;
        }

        public float GetStunChance()
        {
            return this.stunChance;
        }

        public float GetPoisonTime()
        {
            return this.poisonTime;
        }

        public float GetPoisonChance()
        {
            return this.poisonChance;
        }

        public float GetPoisonDamage()
        {
            return this.poisonTickDamage;
        }

        public float GetLifeSteal()
        {
            return this.lifeSteal;
        }

        public IBulletMovement GetBulletMovement()
        {
            return this.bulletMovement;
        }
    }
}
