using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Enemy.Command;

/*
 * Abstract class for enemies. 
 * 
 *     This class is where all methods belong that happen to all enemies. It holds the core mechanics for movement, taking damage, and attacking. 
 *     More specific enemies will inherit this functionality, and add onto it where needed using the 'base' keyword. 
 *     
 *     See MeleeEnemyController for an example on how this inheritance works.
 * 
 */

abstract public class EnemyController : MonoBehaviour
{
    // Stats/misc variables that all enemies will have (Serialize fields are for debug purposes and ironing out game feel)
    [SerializeField] protected float movementSpeed = 1;
    [SerializeField] protected float aggroDistance = 1;
    [SerializeField] protected float attackRange = 0.1f;
    [SerializeField] protected int health = 20;
    [SerializeField] protected int attackDamage = 2;
    [SerializeField] protected float knockback = 50;
    [SerializeField] protected bool attacking = false;
    [SerializeField] private float attackTimer = 2;
    [SerializeField] private float attackBufferTimer = 0;
    [SerializeField] protected float attackBuffer = 1;
    [SerializeField] protected float bulletSpeed = 2;

    [SerializeField] private float hitStunTime = 0.3f;
    [SerializeField] private bool isInHitStun = false;

    //[SerializeField] private GameObject weaponDrop;

    // Reference to the player object
    private Rigidbody2D target;

    // Base movement and attack commands. Set any variable to 'protected' when it needs to be overwritten in child class
    protected IEnemyCommand movement;
    protected IEnemyCommand attack;

    // Start and Update calls virtual method init, that can be extended in inherited classes. Inherited classes will inherit Start/Update methods from this class.
    void Start()
    {
        Init();
    }

    void Update()
    {
        OnUpdate();
    }

    // Actions that every enemy will do on initialization. List any method protection as 'protected' if it will be referenced in child class
    virtual protected void Init()
    {
        this.movement = ScriptableObject.CreateInstance<EnemyWonder>();
        this.attack = ScriptableObject.CreateInstance<DoNothing>();
        AttatchPlayer();
    }

    // Actions that every enemy will do on update
    virtual protected void OnUpdate()
    {
        if (this.health <= 0)
        {
            StartCoroutine(Die());
        }
        else
        {
            if (!this.isInHitStun)
            {
                this.movement.Execute(this.gameObject);

                if (this.attacking == false)
                {
                    var animator = this.gameObject.GetComponent<Animator>();
                    animator.SetFloat("Velocity", Mathf.Max(Mathf.Abs(this.gameObject.GetComponent<Rigidbody2D>().velocity.x), Mathf.Abs(this.gameObject.GetComponent<Rigidbody2D>().velocity.y)));
                }
                else
                {
                    BufferAttack();
                }
            }
        }
    }

    // Misc helper functions that all enemies will be able to use

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            InitiateAttack();
        }

        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            int damage = collision.gameObject.GetComponent<PlayerAttack>().GetDamage();
            TakeDamage(damage);

            StartCoroutine(HitStun());

            Vector2 knockbackDirection = (this.gameObject.GetComponent<Rigidbody2D>().position - collision.gameObject.GetComponent<Rigidbody2D>().position).normalized;
            Knockback(knockbackDirection);

            FindObjectOfType<SoundManager>().PlaySoundEffect("Hit");
        }
    }

    private void AttatchPlayer()
    {
        this.target = FindObjectOfType<PlayerController>().GetComponent<Rigidbody2D>();
    }

    public float GetSpeed()
    {
        return this.movementSpeed;
    }

    public Rigidbody2D GetTarget()
    {
        return this.target;
    }

    protected bool IsInChaseRange()
    {
        if( (FindObjectOfType<PlayerController>().GetComponent<Rigidbody2D>().position - this.gameObject.GetComponent<Rigidbody2D>().position).magnitude < this.aggroDistance)
        {
            return true;
        }
        return false;
    }

    public void InitiateAttack() 
    {
        var animator = this.gameObject.GetComponent<Animator>();
        animator.SetBool("Attacking", true);
        this.attacking = true;
    }

    private void BufferAttack() // Pretty ugly, would like to improve some how
    {
        if(this.attackTimer < 1.0f) // This should be handled by anim controller, but its got a bug :(
        {
            this.attackTimer += Time.deltaTime;
        }
        else
        {
            var animator = this.gameObject.GetComponent<Animator>();
            animator.SetBool("Attacking", false);

            if(this.attackBufferTimer < this.attackBuffer)
            {
                this.attackBufferTimer += Time.deltaTime;
            }
            else
            {
                this.attackBufferTimer = 0;
                this.attackTimer = 0;
                this.attacking = false;
            }
        }
    }
    public int GetAttackDamage()
    {
        return this.attackDamage;
    }

    public float GetAttackRange()
    {
        return this.attackRange;
    }

    public float GetKnockback()
    {
        return this.knockback;
    }

    private void Knockback(Vector2 direction)
    {
        this.gameObject.GetComponent<Rigidbody2D>().velocity = this.knockback * direction;
    }

    private IEnumerator HitStun()
    {
        this.isInHitStun = true;
        yield return new WaitForSeconds(this.hitStunTime);
        this.isInHitStun = false;
    }

    public float GetBulletSpeed()
    {
        return this.bulletSpeed;
    }

    public float GetAggroDistance()
    {
        return this.aggroDistance;
    }

    public void TakeDamage(int damage)
    {
        this.health -= damage;
    }

    private IEnumerator Die()
    {
        var animator = this.gameObject.GetComponent<Animator>();
        animator.SetBool("Dying", true);
        FindObjectOfType<SoundManager>().PlaySoundEffect("Death");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + 1);

        /*var enemyPosition = this.gameObject.GetComponent<Rigidbody2D>().transform.position;
        var drop = (GameObject)Instantiate(this.weaponDrop, new Vector3(enemyPosition.x, enemyPosition.y, enemyPosition.z), new Quaternion());
        drop.GetComponent<SpriteRenderer>().sprite = drop.GetComponent<Weapon>().sprite;*/

        Destroy(this.gameObject);
    }
    public bool IsAttacking()
    {
        return this.attacking;
    }
}