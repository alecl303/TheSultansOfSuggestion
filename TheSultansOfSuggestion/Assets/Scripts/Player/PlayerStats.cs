using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player.Stats
{
    public class PlayerStats : MonoBehaviour
    {
        [SerializeField] public float movementSpeed = 2.0f;
        [SerializeField] public int health = 100;
        [SerializeField] public int maxHealth = 100;
        [SerializeField] public int mana = 100;
        [SerializeField] public int maxMana = 100;
        [SerializeField] public float bulletSpeed = 3;
        [SerializeField] public float bulletLifeSpan = 0.5f;
        [SerializeField] public int rangeDamage = 2;
        [SerializeField] public int meleeDamage = 5;
        [SerializeField] public float fireRate = 0.4f;
        [SerializeField] public List<GameObject> weapons;
        [SerializeField] public GameObject activeWeapon;

        [SerializeField] public int weaponDamage;

        //private PlayerController playerController;
        // Start is called before the first frame update
        void Start()
        {
            //this.playerController = this.gameObject.GetComponent<PlayerController>();
            this.weapons.Add(this.activeWeapon);

            this.weaponDamage = this.activeWeapon.GetComponent<Weapon>().GetDamage();
        }

        public float GetSpeed()
        {
            return this.movementSpeed;
        }

        public void SetMovementSpeed(float speed)
        {
            this.movementSpeed = speed;
        }

        public float GetBulletSpeed()
        {
            return this.bulletSpeed;
        }

        public float GetBulletRange()
        {
            return this.bulletLifeSpan;
        }

        public int GetRangeDamage()
        {
            return this.rangeDamage;
        }

        public int GetMeleeDamage()
        {
            this.weaponDamage = this.activeWeapon.GetComponent<Weapon>().GetDamage();
            Debug.Log(this.weaponDamage);
            Debug.Log(this.activeWeapon.GetComponent<Weapon>().GetDamage());
            return this.meleeDamage + this.weaponDamage;  // Will have to figure out active weapon in inventory
        }

        public void SetRangeDamage(int damage)
        {
            this.rangeDamage = damage;
        }

        public void Heal(int amount)
        {
            this.health += Mathf.Min(amount, this.maxHealth - this.health);
        }

        public int GetMana()
        {
            return this.mana;
        }

        public void DrainMana(int amount)
        {
            this.mana -= amount;
        }

        public float GetFireRate()
        {
            return this.fireRate;
        }
    }
}
