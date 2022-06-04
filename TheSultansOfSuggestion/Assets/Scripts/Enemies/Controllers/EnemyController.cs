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
    [SerializeField] protected float movementSpeed = 3;
    [SerializeField] protected float aggroDistance = 5;
    [SerializeField] protected float attackRange = 0.3f;
    [SerializeField] protected float health = 20;
    [SerializeField] protected float attackDamage = 2;
    [SerializeField] protected float knockback = 10;
    [SerializeField] protected bool attacking = false;
    [SerializeField] protected float attackBuffer = 2;
    [SerializeField] protected float bulletSpeed = 2;

    [SerializeField] private float hitStunTime = 0.3f;
    [SerializeField] private bool isInHitStun = false;
    [SerializeField] private bool isFlying = false;
    [SerializeField] public GameObject bulletPrefab;


    [SerializeField] private Texture2D crosshair;
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;
    
    private bool dying = false;
    [SerializeField] private bool canAct = true;

    
    
    //[SerializeField] private GameObject weaponDrop;

    [SerializeField] private bool isDead = false;
    // Reference to the player object
    private Rigidbody2D target;
    
    private PlayerController targetRage;

    // Base movement and attack commands. Set any variable to 'protected' when it needs to be overwritten in child class
    protected IEnemyCommand movement;
    protected IEnemyCommand attack;
    protected IEnemyCommand chase;

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
        AttachPlayer();
        FindObjectOfType<EnemySpawner>().liveEnemies += 1;
    }

    // Actions that every enemy will do on update
    virtual protected void OnUpdate()
    {
        if (this.health <= 0 && !this.dying)
        {
            this.canAct = false;
            this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            StopAllCoroutines();
            StartCoroutine(Die());
        }
        else if(this.canAct)
        {
            if (!this.isInHitStun)
            {
                this.movement.Execute(this.gameObject);
                var animator = this.gameObject.GetComponent<Animator>();
                animator.SetFloat("Velocity", Mathf.Max(Mathf.Abs(this.gameObject.GetComponent<Rigidbody2D>().velocity.x), Mathf.Abs(this.gameObject.GetComponent<Rigidbody2D>().velocity.y)));
            }
            if (this.IsInChaseRange())
            {
                this.movement = this.chase;
            }

            

            if ((this.gameObject.GetComponent<Rigidbody2D>().position - this.GetTarget().position).magnitude < this.GetAttackRange() && !this.IsAttacking())
            {
                this.attack.Execute(this.gameObject);
                if (isFlying)
                {
                    StartCoroutine(InitiateAttack());
                }
            }
        }
    }

    // Misc helper functions that all enemies will be able to use

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !this.attacking && !collision.gameObject.GetComponent<PlayerController>().IsInIFrame())
        {
            StartCoroutine(InitiateAttack());
            StartCoroutine(InitiateBuffer(collision));
        }

        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            var attackObject = collision.gameObject.GetComponent<PlayerAttack>();
            float damage = attackObject.GetDamage();

            TakeDamage(damage);
            StartCoroutine(HitStun());

            //Vector2 knockbackDirection = (this.gameObject.GetComponent<Rigidbody2D>().position - collision.gameObject.GetComponent<Rigidbody2D>().position).normalized;
            //Knockback(knockbackDirection);

            FindObjectOfType<SoundManager>().PlaySoundEffect("Hit");

            CheckForStun(attackObject);
            CheckForPoison(attackObject);
            CheckForLifeDrain(attackObject);
        }

        if (collision.gameObject.CompareTag("PlayerMeleeAttack"))
        {
            var attackObject = collision.gameObject.GetComponent<PlayerAttack>();
            float damage = attackObject.GetDamage();

            TakeDamage(damage);
            StartCoroutine(HitStun());

            Vector2 knockbackDirection = (this.gameObject.GetComponent<Rigidbody2D>().position - collision.gameObject.GetComponent<Rigidbody2D>().position).normalized;
            Knockback(knockbackDirection);

            FindObjectOfType<SoundManager>().PlaySoundEffect("Hit");

            CheckForStun(attackObject);
            CheckForPoison(attackObject);
            CheckForLifeDrain(attackObject);
        }

        if (collision.gameObject.CompareTag("Hole"))
        {
            if (this.isFlying)
            {
                Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !this.attacking && !collision.gameObject.GetComponent<PlayerController>().IsInIFrame())
        {
            StartCoroutine(InitiateBuffer(collision));
            StartCoroutine(InitiateAttack());
        }

        //if (collision.gameObject.CompareTag("PlayerAttack"))
        //{
        //    var attackObject = collision.gameObject.GetComponent<PlayerAttack>();
        //    float damage = attackObject.GetDamage();

        //    TakeDamage(damage);
        //    StartCoroutine(HitStun());

        //    //Vector2 knockbackDirection = (this.gameObject.GetComponent<Rigidbody2D>().position - collision.gameObject.GetComponent<Rigidbody2D>().position).normalized;
        //    //Knockback(knockbackDirection);

        //    FindObjectOfType<SoundManager>().PlaySoundEffect("Hit");

        //    CheckForStun(attackObject);
        //    CheckForPoison(attackObject);
        //    CheckForLifeDrain(attackObject);
        //}
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("PlayerTrapSpell"))
        {
            other.gameObject.GetComponent<IEnemyTrapSpellEffect>().ApplyEffect(this);
        }
    }

    private void AttachPlayer()
    {
        var temp = FindObjectOfType<PlayerController>();
        this.target = temp.GetComponent<Rigidbody2D>();
        this.targetRage = temp.GetComponent<PlayerController>();
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

    public IEnumerator InitiateAttack() 
    {
        var animator = this.gameObject.GetComponent<Animator>();

        animator.SetBool("Attacking", true);
        this.attacking = true;

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        animator.SetBool("Attacking", false);
        yield return new WaitForSeconds(this.attackBuffer);
        this.attacking = false;
    }

    public IEnumerator InitiateBuffer(Collision2D collision)
    {
        var oldChase = this.chase;
        this.chase = ScriptableObject.CreateInstance<DoNothing>();
        float waitTime = this.attackBuffer + collision.gameObject.GetComponent<PlayerController>().GetIFrameTime();
        yield return new WaitForSeconds(waitTime);
        this.chase = oldChase;
    }

    public float GetAttackDamage()
    {
        return this.attackDamage;
    }

    public float GetAttackRange()
    {
        return this.attackRange;
    }

    //public float GetKnockback()
    //{
    //    return this.knockback;
    //}

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

    private void CheckForStun(PlayerAttack attack)
    {
        var stunChance = attack.GetStunChance();

        if (Random.Range(1, 100) <= stunChance)
        {
            StartCoroutine(Stun(attack.GetStunTime()));
        }
    }
    // InflictStun is used for just spells 
    public void InflictStun(float stunTime)
    {
        StartCoroutine(Stun(stunTime));
    }
    // Stun the enemy, and set them blue
    private IEnumerator Stun(float stunTime)
    {
        // Take damage will occur first for 0.15 seconds, so wait .2 seconds
        yield return new WaitForSeconds(0.2f);

        // Call x number of times and keep changing the color to blue. This means enemy is still
        var numCalls = 16;
        for (int i = 0; i < numCalls; i++)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 255);
            this.canAct = false;
            yield return new WaitForSeconds(stunTime/numCalls);
        }
        this.canAct = true;
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255,255,255);
    }

    private void CheckForPoison(PlayerAttack attack)
    {
        var poisonChance = attack.GetPoisonChance();

        if (Random.Range(1, 100) <= poisonChance)
        {
            StartCoroutine(Poison(attack.GetPoisonTime()));
        }
    }

    private IEnumerator Poison(float poisonTime)
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 255, 0);
        var tickDamage = this.target.gameObject.GetComponent<PlayerController>().GetStats().GetPoisonDamage();
        // Tick damage for poison
        for (int i =  0; i < poisonTime; i++)
        {
            yield return new WaitForSeconds(1);
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 255, 0);
            TakeDamage(tickDamage);
        }
        // Wait 0.5 seconds, since TakeDamage will revert color after 0.15 seconds. This will ensure the color below is last to run
        yield return new WaitForSeconds(0.2f);
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
    }

    private void CheckForLifeDrain(PlayerAttack attack)
    {
        if (attack.CanStealHp())
        {
            var playerStats = this.target.gameObject.GetComponent<PlayerController>().GetStats();
            playerStats.Heal(playerStats.GetLifeSteal());
        }
    }

    public float GetBulletSpeed()
    {
        return this.bulletSpeed;
    }

    public float GetAggroDistance()
    {
        return this.aggroDistance;
    }

    // Make enemy flash red for 0.15 seconds when taking damage 
    private IEnumerator FlashStart()
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0);

        yield return new WaitForSeconds(0.15f);

        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
    }

    public void TakeDamage(float damage)
    {
        this.health -= damage;
        if(this.health <= 0 && !isDead){
            this.isDead = true;
            var playerStats = this.target.gameObject.GetComponent<PlayerController>().GetStats();
            playerStats.IncrementRage();
        }
        else
        {
            StartCoroutine(FlashStart());
        }
    }

    private IEnumerator Die()
    {
        var animator = this.gameObject.GetComponent<Animator>();
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        animator.SetBool("Dying", true);
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);

        FindObjectOfType<SoundManager>().PlaySoundEffect("Death");

        this.dying = true;
        FindObjectOfType<EnemySpawner>().liveEnemies -= 1;
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + 3);

        /*var enemyPosition = this.gameObject.GetComponent<Rigidbody2D>().transform.position;
        var drop = (GameObject)Instantiate(this.weaponDrop, new Vector3(enemyPosition.x, enemyPosition.y, enemyPosition.z), new Quaternion());
        drop.GetComponent<SpriteRenderer>().sprite = drop.GetComponent<Weapon>().sprite;*/
        Destroy(this.gameObject);
    }
    public bool IsAttacking()
    {
        return this.attacking;
    }

    public void OnMouseEnter()
    {
        Cursor.SetCursor(crosshair, new Vector2(crosshair.width/2, crosshair.height/2), cursorMode);
    }
    public void OnMouseExit()
    {
        Cursor.SetCursor(null, new Vector2(0,0), cursorMode);
    }
}