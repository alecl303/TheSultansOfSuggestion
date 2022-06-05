using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Bullet.Command;

namespace Player.Stats
{
    public class PlayerStats : MonoBehaviour
    {
        public float movementSpeed = 6.0f;
        public float health = 100;
        public float maxHealth = 100;
        public float mana = 100;
        public float maxMana = 100;
        public float manaRechargeRate = .02f;
        public float rage = 0;
        public float maxRage = 10;
        public float bulletSpeed = 9;
        public float bulletLifeSpan = 0.7f;
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
        public int critChance = 2;
        public float critMultiplier = 1.5f;
        public bool manaIsHp = false;
        //public List<GameObject> weapons;
        //public GameObject activeWeapon;



        public List<Weapon> weapons;
        public Weapon activeWeapon;

        public IBulletMovement bulletMovement;

        public GameObject freezeBox;
        public GameObject whirlwind;
        public GameObject healCircle;
        public GameObject icicleTrap;

        public int weaponDamage;

        private GameObject healthBar;
        private GameObject manaBar;
        private GameObject rageBar;

        public GameObject weaponSprite;

        public Sprite testSprite;

        void Awake()
        {
            this.activeWeapon = gameObject.AddComponent<Weapon>();
            this.activeWeapon.SetSprite(this.gameObject.GetComponent<WeaponSprites>().sprites[this.activeWeapon.spriteIndex]);
            this.weapons.Add(this.activeWeapon);

            this.weaponDamage = this.activeWeapon.GetComponent<Weapon>().GetDamage();

            this.bulletMovement = ScriptableObject.CreateInstance<StandardBullet>();

            this.healthBar = GameObject.Find("/HUD/HealthBar");
            this.manaBar = GameObject.Find("/HUD/Mana");
            this.rageBar = GameObject.Find("/HUD/Rage");

            //this.weaponSprite = GameObject.Find("/HUD/Item_slot/slot/Border/Item_sprite");
            //this.weaponSprite.GetComponent<Image>().sprite = this.activeWeapon.GetComponent<Weapon>().sprite;
        }

        private void Update()
        {
            RegenMana();
            //CheckRage();
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

        public float GetCritMultiplier()
        {
            if (Random.Range(0, 100) <= this.critChance)
            {
                Debug.Log("Crit!"); // TODO: Insert UI logic
                return this.critMultiplier;
            }
            else
            {
                return 1;
            }
        }

        public float GetMeleeDamage()
        {
            this.weaponDamage = this.activeWeapon.GetComponent<Weapon>().GetDamage();

            var damage = this.meleeDamage + this.weaponDamage;

            return damage;  // Will have to figure out active weapon in inventory
        }

        public void SetMaxHealth(float maxHealth)
        {
            this.maxHealth = maxHealth;
            this.health = Mathf.Clamp(this.health, 0, this.maxHealth);
            // Update health bar indication
            this.healthBar.GetComponent<Slider>().value = (float)this.health / (float)this.maxHealth;
        }

        public void Heal(float amount)
        {
            this.health += Mathf.Min(amount, this.maxHealth - this.health);
            // Update health bar indication
            this.healthBar.GetComponent<Slider>().value = (float)this.health / (float)this.maxHealth;
        }

        public void TakeDamage(float damage)
        {
            this.health -= damage;
            this.healthBar.GetComponent<Slider>().value = (float)this.health / (float)this.maxHealth;
        }

        public float GetMana()
        {
            return this.mana;
        }

        public void DrainMana(float amount)
        {
            if (this.manaIsHp)
            {
                this.health -= amount * 0.2f;
                this.healthBar.GetComponent<Slider>().value = (float)this.health / (float)this.maxHealth;

            }
            else
            {
                this.mana -= amount;
                this.manaBar.GetComponent<Slider>().value = Mathf.Min(this.mana,0) / this.maxMana;
            }
        }

        public void RegenMana()
        {
            this.mana += Mathf.Min(this.manaRechargeRate, this.maxMana - this.mana);
            this.manaBar.GetComponent<Slider>().value = this.mana / this.maxMana;
        }

        // public void CheckRage()
        // {
        //     this.rageBar.GetComponent<Slider>().value = this.rage / this.maxRage;
        // }

        public void IncrementRage()
        {
            if (this.rage < this.maxRage)
            {
                this.rage += 1;
            }
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
        public IEnumerator BerserkForXSeconds(float duration)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 0.8f);
            this.meleeDamage *= 2;
            this.rangeDamage *= 2;
            yield return new WaitForSeconds(duration);
            this.meleeDamage *= 0.5f;
            this.rangeDamage *= 0.5f;
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1);
        }

        public void SetActiveWeapon(Weapon weapon)
        {
            this.activeWeapon = weapon;
            //this.activeWeapon.SetSprite(this.gameObject.GetComponent<WeaponSprites>().sprites[this.activeWeapon.spriteIndex]);
            this.weapons.Add(this.activeWeapon);

            this.weaponDamage = this.activeWeapon.GetComponent<Weapon>().GetDamage();
        }
    }
}
