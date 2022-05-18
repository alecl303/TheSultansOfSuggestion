using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Command;
using Player.Effect;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 2.0f;
    [SerializeField] private int health = 100;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private float bulletSpeed = 3;
    [SerializeField] private int rangeDamage = 2;
    [SerializeField] private int meleeDamage = 5;
    [SerializeField] private float fireRate = 1;

    [SerializeField] private float iFrameTime = 1;
    [SerializeField] private bool isInIFrame = false;
    [SerializeField] private float hitStunTime = 0.3f;
    [SerializeField] private bool isInHitStun = false;
    [SerializeField] private float attackTime = 2;
    [SerializeField] private bool isAttacking = false;
    [SerializeField] private bool isDead = false;
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public GameObject hitboxPrefab;
    [SerializeField] public List<Weapon> weapons;
    [SerializeField] public Weapon activeWeapon;

    private IPlayerCommand fire1;
    private IPlayerCommand fire2;
    private IPlayerCommand right;
    private IPlayerCommand left;
    private IPlayerCommand up;
    private IPlayerCommand down;

    // Start is called before the first frame update
    void Start()
    {

        this.fire1 = ScriptableObject.CreateInstance<RangedAttack>();
        this.fire2 = ScriptableObject.CreateInstance<MeleeAttack>();
        this.right = ScriptableObject.CreateInstance<MoveCharacterRight>();
        this.left = ScriptableObject.CreateInstance<MoveCharacterLeft>();
        this.up = ScriptableObject.CreateInstance<MoveCharacterUp>();
        this.down = ScriptableObject.CreateInstance<MoveCharacterDown>();

        this.weapons.Add(this.activeWeapon);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.health <= 0)
        {
            StartCoroutine(Die());
        }
        else
        {
            if (!this.isInHitStun)
            {
                if (Input.GetAxis("Horizontal") > 0.01)
                {
                    this.right.Execute(this.gameObject);
                }
                if (Input.GetAxis("Horizontal") < -0.01)
                {
                    this.left.Execute(this.gameObject);
                }
                if (Input.GetAxis("Vertical") < -0.01)
                {
                    this.down.Execute(this.gameObject);
                }
                if (Input.GetAxis("Vertical") > 0.01)
                {
                    this.up.Execute(this.gameObject);
                }

                if (!this.isAttacking)
                {
                    if (Input.GetButtonDown("Fire1"))
                    {
                        this.fire1.Execute(this.gameObject);
                    }
                    if (Input.GetButtonDown("Fire2"))
                    {
                        this.fire2.Execute(this.gameObject);
                    }
                }

                var animator = this.gameObject.GetComponent<Animator>();
                animator.SetFloat("Velocity", Mathf.Max(Mathf.Abs(this.gameObject.GetComponent<Rigidbody2D>().velocity.x), Mathf.Abs(this.gameObject.GetComponent<Rigidbody2D>().velocity.y)));
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!this.isInIFrame)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                var playerRigidBody = this.gameObject.GetComponent<Rigidbody2D>();
                var enemy = collision.gameObject.GetComponent<EnemyController>();

                playerRigidBody.velocity = (enemy.GetKnockback() * (playerRigidBody.position - collision.gameObject.GetComponent<Rigidbody2D>().position).normalized);

                TakeDamage(enemy.GetAttackDamage());

                FindObjectOfType<SoundManager>().PlaySoundEffect("Melee");

                StartCoroutine(HitStun());
                StartCoroutine(IFrame());
            }

            if (collision.gameObject.CompareTag("EnemyAttack"))
            {
                var playerRigidBody = this.gameObject.GetComponent<Rigidbody2D>();
                var bulletKnockBack = 10; // temporary

                playerRigidBody.velocity = (bulletKnockBack * (playerRigidBody.position - collision.gameObject.GetComponent<Rigidbody2D>().position).normalized);

                TakeDamage(collision.gameObject.GetComponent<EnemyAttack>().GetDamage());

                FindObjectOfType<SoundManager>().PlaySoundEffect("EnemyFire");

                StartCoroutine(HitStun());
                StartCoroutine(IFrame());
            }
        }
        else
        {
            if (collision.gameObject.CompareTag("EnemyAttack"))
            {
                Destroy(collision.gameObject);
            }
        }

        // Temporary weapon pick up logic
        if (collision.gameObject.CompareTag("Item"))
        {
            this.weapons.Add(collision.gameObject.GetComponent<Weapon>());
            Destroy(collision.gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        this.health -= damage;
        this.isInHitStun = true;
    }

    public float GetSpeed()
    {
        return this.movementSpeed;
    }

    public void SetMovementSpeed(float speed)
    {
        this.movementSpeed = speed;
    }

    private IEnumerator HitStun()
    {
        this.isInHitStun = true;
        yield return new WaitForSeconds(this.hitStunTime);
        this.isInHitStun = false;
    }

    public void IsAttacking()
    {
        this.isAttacking = true;
        StartCoroutine(Attacking());
        //this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    private IEnumerator Attacking()
    {
        var animator = this.gameObject.GetComponent<Animator>();
        animator.SetBool("Attacking", true);
        yield return new WaitForSeconds(this.attackTime);
        animator.SetBool("Attacking", false);
        this.isAttacking = false;
    }

    public float GetBulletSpeed()
    {
        return this.bulletSpeed;
    }

    public int GetRangeDamage()
    {
        return this.rangeDamage;
    }

    public int GetMeleeDamage()
    {
        return this.meleeDamage + this.weapons[0].GetDamage(); // Will have to figure out active weapon in inventory
    }

    public void SetRangeDamage(int damage)
    {
        this.rangeDamage = damage;
    }
    
    public void Heal(int amount)
    {
        this.health += Mathf.Min(amount, this.maxHealth - this.health);
    }

    public void ExecuteEffect(IPlayerEffect effect)
    {
        effect.Execute(this.gameObject);
    }

    public float GetFireRate()
    {
        return this.fireRate;
    }
    private IEnumerator IFrame()
    {
        this.isInIFrame = true;
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0.5f);

        yield return new WaitForSeconds(this.iFrameTime);

        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        this.isInIFrame = false;
    }

    private IEnumerator Die()
    {
        if (!this.isDead)
        {
            var animator = this.gameObject.GetComponent<Animator>();
            animator.SetBool("Dead", true);
            FindObjectOfType<SoundManager>().PlaySoundEffect("Death");
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + 1);

            FindObjectOfType<SoundManager>().PlayMusicTrack("Game Over Long");
        }
    }
}