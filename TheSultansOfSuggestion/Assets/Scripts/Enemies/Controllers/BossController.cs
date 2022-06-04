using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Boss.Command;

/*
 * Abstract class for enemies. 
 * 
 *     This class is where all methods belong that happen to all enemies. It holds the core mechanics for movement, taking damage, and attacking. 
 *     More specific enemies will inherit this functionality, and add onto it where needed using the 'base' keyword. 
 *     
 *     See MeleeEnemyController for an example on how this inheritance works.
 * 
 */

public class BossController : MonoBehaviour
{
    // Stats/misc variables that all enemies will have (Serialize fields are for debug purposes and ironing out game feel)
    [SerializeField] protected float movementSpeed = 3;
    [SerializeField] protected float health = 200;
    [SerializeField] protected float maxHealth = 200;
    [SerializeField] protected bool attacking = false;
    [SerializeField] protected float attackBuffer = 2;
    [SerializeField] protected float bulletSpeed = 2;
    [SerializeField] private float attackDamage = 2;
    [SerializeField] private bool isFlying = true;
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public GameObject targetBulletPrefab;

    [SerializeField] private Texture2D crosshair;
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;

    private bool dying = false;
    private bool canAct = true;

    [SerializeField] private bool isDead = false;
   
    private Rigidbody2D target;

    private PlayerController targetRage;

    // Base movement and attack commands. Set any variable to 'protected' when it needs to be overwritten in child class
    protected IBossCommand movement;
    protected List<IBossCommand> attacks;

    private IBossCommand rangedAttack1;
    private IBossCommand stompAttack;
    private IBossCommand rangedAttack2;

    protected IBossCommand chase;

    [SerializeField] private int currentAttackIndex = 0;

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
    protected void Init()
    {
        this.movement = ScriptableObject.CreateInstance<DoNothingBoss>();
        this.attacks = new List<IBossCommand>{
            ScriptableObject.CreateInstance<BulletSpawn4>(),
            ScriptableObject.CreateInstance<BulletSpawn3>(),
            ScriptableObject.CreateInstance<BulletSpawn2>(),
            ScriptableObject.CreateInstance<BulletSpawn1>(),
        };
        this.rangedAttack1 = ScriptableObject.CreateInstance<BulletSpawn2>();
        this.stompAttack = ScriptableObject.CreateInstance<DoNothingBoss>();
        this.rangedAttack2 = ScriptableObject.CreateInstance<BulletSpawn1>();
        AttachPlayer();
        //FindObjectOfType<EnemySpawner>().liveEnemies += 1;
    }

    // Actions that every enemy will do on update
    protected void OnUpdate()
    {
        if (this.health <= 0 && !this.dying)
        {
            this.canAct = false;
            this.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            StartCoroutine(Die());
        }
        else if (this.canAct)
        {
            this.movement.Execute(this.gameObject);
            var animator = this.gameObject.GetComponent<Animator>();
            animator.SetFloat("Velocity", Mathf.Max(Mathf.Abs(this.gameObject.GetComponent<Rigidbody2D>().velocity.x), Mathf.Abs(this.gameObject.GetComponent<Rigidbody2D>().velocity.y)));
           
            this.attacks[this.currentAttackIndex].Execute(this.gameObject);
            if (!this.attacking)
            {
                StartCoroutine(InitiateAttack(this.attacks[this.currentAttackIndex].GetDuration()));
            }
        }

        //SetState();
    }

    // Misc helper functions that all enemies will be able to use

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            var attackObject = collision.gameObject.GetComponent<PlayerAttack>();
            float damage = attackObject.GetDamage();
            bool isCrit = attackObject.IsCrit();
            var popup = DamageNumber.CreatePopup(this.gameObject.GetComponent<Rigidbody2D>().position, damage, isCrit);

            TakeDamage(damage);

            FindObjectOfType<SoundManager>().PlaySoundEffect("Hit");

            CheckForStun(attackObject);
            CheckForPoison(attackObject);
            CheckForLifeDrain(attackObject);
        }

        if (collision.gameObject.CompareTag("PlayerMeleeAttack"))
        {
            var attackObject = collision.gameObject.GetComponent<PlayerAttack>();
            float damage = attackObject.GetDamage();
            bool isCrit = attackObject.IsCrit();
            var popup = DamageNumber.CreatePopup(this.gameObject.GetComponent<Rigidbody2D>().position, damage, isCrit);

            TakeDamage(damage);

            FindObjectOfType<SoundManager>().PlaySoundEffect("Hit");

            CheckForStun(attackObject);
            CheckForPoison(attackObject);
            CheckForLifeDrain(attackObject);
        }
    }

    private void SetState()
    {

        if (this.health == this.maxHealth)
        {
            this.attacks = new List<IBossCommand>{
                this.rangedAttack1,
                this.rangedAttack2
            };
        }
        else if(this.health < 75)
        {
            this.attacks = new List<IBossCommand>{
                this.rangedAttack1,
                this.rangedAttack2
            };
            this.movement = ScriptableObject.CreateInstance<DoNothingBoss>();
        }
        else if(this.health < 50)
        {
            this.attacks = new List<IBossCommand>{
                this.rangedAttack1,
                this.rangedAttack2
            };
        }
        else
        {
            this.attacks = new List<IBossCommand>{
                this.rangedAttack1,
                this.rangedAttack2
            };
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

    public IEnumerator InitiateAttack(float duration)
    {
        var animator = this.gameObject.GetComponent<Animator>();
        animator.SetBool("Attacking", true);
        this.attacking = true;

        yield return new WaitForSeconds(duration);

        animator.SetBool("Attacking", false);
        yield return new WaitForSeconds(this.attackBuffer);

        this.currentAttackIndex = (this.currentAttackIndex + 1) % this.attacks.Count;
        this.attacking = false;
    }

    public float GetAttackDamage()
    {
        return this.attackDamage;
    }

    private void CheckForStun(PlayerAttack attack)
    {
        var stunChance = attack.GetStunChance();

        if (Random.Range(1, 100) <= stunChance)
        {
            StartCoroutine(Stun(attack.GetStunTime()));
        }
    }
    public void InflictStun(float stunTime)
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 255);
        this.canAct = false;
        StartCoroutine(Stun(stunTime));
    }
    private IEnumerator Stun(float stunTime)
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 255);
        this.canAct = false;
        yield return new WaitForSeconds(stunTime);
        this.canAct = true;
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
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
        for (int i = 0; i < poisonTime; i++)
        {
            yield return new WaitForSeconds(1);
            TakeDamage(tickDamage);
        }

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

    private void FlashStart()
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 1);
        Invoke("FlashStop", 0.3f);
    }

    private void FlashStop()
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
    }

    public void TakeDamage(float damage)
    {
        this.health -= damage;
        if (this.health <= 0 && !isDead)
        {
            this.isDead = true;
            var playerStats = this.target.gameObject.GetComponent<PlayerController>().GetStats();
            playerStats.IncrementRage();
        }
        else
        {
            FlashStart();
        }
    }

    private IEnumerator Die()
    {
        var animator = this.gameObject.GetComponent<Animator>();
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        //animator.SetBool("Dying", true);
        FindObjectOfType<SoundManager>().PlaySoundEffect("Death");

        this.dying = true;
        //FindObjectOfType<EnemySpawner>().liveEnemies -= 1;
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + 3);

        Destroy(this.gameObject);
    }
    public bool IsAttacking()
    {
        return this.attacking;
    }

    public void OnMouseEnter()
    {
        Cursor.SetCursor(crosshair, new Vector2(crosshair.width / 2, crosshair.height / 2), cursorMode);
    }
    public void OnMouseExit()
    {
        Cursor.SetCursor(null, new Vector2(0, 0), cursorMode);
    }
}