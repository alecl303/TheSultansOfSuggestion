using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using Player.Command;
using Player.Effect;
using Player.Stats;
public class PlayerController : MonoBehaviour
{
    private PlayerStats stats;

    [SerializeField] private bool canShoot = true;
    [SerializeField] private float iFrameTime = 5f;
    [SerializeField] private bool isInIFrame = false;
    [SerializeField] private float hitStunTime = 0.3f;
    [SerializeField] private bool isInHitStun = false;
    [SerializeField] private float attackTime = 2;
    [SerializeField] private bool isAttacking = false;
    [SerializeField] private bool isDead = false;
    [SerializeField] private bool isEnabled = true;
    [SerializeField] private bool canDodge = true;
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public GameObject hitboxPrefab;
    [SerializeField] private Sprite empty;
    [SerializeField] public IPlayerSpell activeSpell1;
    private bool onCooldown = false;

    private ItemBar playersCurrentItemBar;

    private IPlayerCommand fire1;
    private IPlayerCommand fire2;
    private IPlayerCommand right;
    private IPlayerCommand left;
    private IPlayerCommand up;
    private IPlayerCommand down;
    private IPlayerCommand roll;

    // Diagonal movement
    private IPlayerCommand upRight;
    private IPlayerCommand upLeft;
    private IPlayerCommand downRight;
    private IPlayerCommand downLeft;
    // Start is called before the first frame update

    // For keeping track of collision iframe 
    private Coroutine iframeRoutine;

    void Start()
    {
        this.stats = this.gameObject.GetComponent<PlayerStats>();
        this.playersCurrentItemBar = this.gameObject.GetComponent<ItemBar>();
        this.fire1 = ScriptableObject.CreateInstance<RangedAttack>();
        this.fire2 = ScriptableObject.CreateInstance<MeleeAttack>();
        this.right = ScriptableObject.CreateInstance<MoveCharacterRight>();
        this.left = ScriptableObject.CreateInstance<MoveCharacterLeft>();
        this.up = ScriptableObject.CreateInstance<MoveCharacterUp>();
        this.down = ScriptableObject.CreateInstance<MoveCharacterDown>();
        this.upRight = ScriptableObject.CreateInstance<MoveCharacterUpRight>();
        this.upLeft = ScriptableObject.CreateInstance<MoveCharacterUpLeft>();
        this.downRight = ScriptableObject.CreateInstance<MoveCharacterDownRight>();
        this.downLeft = ScriptableObject.CreateInstance<MoveCharacterDownLeft>();
        this.activeSpell1 = ScriptableObject.CreateInstance<SpellNothing>();
        
        this.roll = ScriptableObject.CreateInstance<Roll>();

        this.playersCurrentItemBar.Updateslots(0, this.stats.activeWeapon.GetComponent<Weapon>().sprite);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.stats.health <= 0)
        {
            StartCoroutine(Die());
        }
        else
        {
            if (this.isEnabled)
            {
                if (!this.isInHitStun)
                {
                    // Check for movement if diagional or just one direction.
                    if (Input.GetAxis("Horizontal") > 0.01 && Input.GetAxis("Vertical") < -0.01)
                    {
                        this.downRight.Execute(this.gameObject);
                    }
                    else if (Input.GetAxis("Horizontal") > 0.01 && Input.GetAxis("Vertical") > 0.01)
                    {
                        this.upRight.Execute(this.gameObject);
                    }
                    else if (Input.GetAxis("Horizontal") < -0.01 && Input.GetAxis("Vertical") < -0.01)
                    {
                        this.downLeft.Execute(this.gameObject);
                    }
                    else if (Input.GetAxis("Horizontal") < -0.01 && Input.GetAxis("Vertical") > 0.01)
                    {
                        this.upLeft.Execute(this.gameObject);
                    }
                    else 
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
                    }

                    if (!this.isAttacking)
                    {
                        // Ranged Attack
                        if (Input.GetButton("Fire1") && this.canShoot)
                        {
                            this.fire1.Execute(this.gameObject);
                            StartCoroutine(Shooting());
                        }

                        //Melee Attack
                        if (Input.GetButtonDown("Fire2"))
                        {
                            this.fire2.Execute(this.gameObject);
                        }
                        // Active Spell Attack
                        if (Input.GetButtonDown("Fire3"))
                        {
                            if (!onCooldown) {
                                this.onCooldown = true;
                                this.activeSpell1.Execute(this.gameObject);
                                var cooldownTime = this.activeSpell1.GetCooldown();
                                StartCoroutine(WaitCoolDown(cooldownTime));
                            }
                        }
                        // Dodge roll
                        if (Input.GetButtonDown("Jump") && this.canDodge)
                        {
                            this.roll.Execute(this.gameObject);
                        }
                        
                    }

                    var animator = this.gameObject.GetComponent<Animator>();
                    animator.SetFloat("Velocity", Mathf.Max(Mathf.Abs(this.gameObject.GetComponent<Rigidbody2D>().velocity.x), Mathf.Abs(this.gameObject.GetComponent<Rigidbody2D>().velocity.y)));
                }
            }
            //this.RegenMana();
            //this.CheckRage();
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("PlayerBuffSpell"))
        {
            other.gameObject.GetComponent<IPlayerFloorSpellEffect>().SetOverlap(true);
            other.gameObject.GetComponent<IPlayerFloorSpellEffect>().ApplyEffect(this);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerBuffSpell"))
        {
            other.gameObject.GetComponent<IPlayerFloorSpellEffect>().SetOverlap(false);
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

                //playerRigidBody.velocity = (enemy.GetKnockback() * (playerRigidBody.position - collision.gameObject.GetComponent<Rigidbody2D>().position).normalized);

                stats.TakeDamage(enemy.GetAttackDamage());

                FindObjectOfType<SoundManager>().PlaySoundEffect("Melee");

                //StartCoroutine(HitStun());
                this.iframeRoutine = StartCoroutine(IFrame());
            }

            if (collision.gameObject.CompareTag("Boss"))
            {
                var playerRigidBody = this.gameObject.GetComponent<Rigidbody2D>();
                var enemy = collision.gameObject.GetComponent<BossController>();

                //playerRigidBody.velocity = (enemy.GetKnockback() * (playerRigidBody.position - collision.gameObject.GetComponent<Rigidbody2D>().position).normalized);

                stats.TakeDamage(enemy.GetAttackDamage());

                FindObjectOfType<SoundManager>().PlaySoundEffect("Melee");

                //StartCoroutine(HitStun());
                this.iframeRoutine = StartCoroutine(IFrame());
            }

            if (collision.gameObject.CompareTag("EnemyAttack"))
            {
                var playerRigidBody = this.gameObject.GetComponent<Rigidbody2D>();
                var bulletKnockBack = 10; // temporary

                //playerRigidBody.velocity = (bulletKnockBack * (playerRigidBody.position - collision.gameObject.GetComponent<Rigidbody2D>().position).normalized);

                stats.TakeDamage(collision.gameObject.GetComponent<EnemyAttack>().GetDamage());

                FindObjectOfType<SoundManager>().PlaySoundEffect("EnemyFire");

                //StartCoroutine(HitStun());
                this.iframeRoutine = StartCoroutine(IFrame());

                Destroy(collision.gameObject);
            }

            // if (collision.gameOBject.CompareTag(""))
        }
        else
        {
            if (collision.gameObject.CompareTag("EnemyAttack"))
            {
                Destroy(collision.gameObject);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!this.isInIFrame)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                var playerRigidBody = this.gameObject.GetComponent<Rigidbody2D>();
                var enemy = collision.gameObject.GetComponent<EnemyController>();

                //playerRigidBody.velocity = (enemy.GetKnockback() * (playerRigidBody.position - collision.gameObject.GetComponent<Rigidbody2D>().position).normalized);

                TakeDamage(enemy.GetAttackDamage());

                FindObjectOfType<SoundManager>().PlaySoundEffect("Melee");

                //StartCoroutine(HitStun());
                StartCoroutine(IFrame());
            }

            //if (collision.gameObject.CompareTag("EnemyAttack"))
            //{
            //    var playerRigidBody = this.gameObject.GetComponent<Rigidbody2D>();
            //    var bulletKnockBack = 10; // temporary

            //    //playerRigidBody.velocity = (bulletKnockBack * (playerRigidBody.position - collision.gameObject.GetComponent<Rigidbody2D>().position).normalized);

            //    TakeDamage(collision.gameObject.GetComponent<EnemyAttack>().GetDamage());

            //    FindObjectOfType<SoundManager>().PlaySoundEffect("EnemyFire");

            //    //StartCoroutine(HitStun());
            //    StartCoroutine(IFrame());

            //    Destroy(collision.gameObject);
            //}
        }
        //else
        //{
        //    if (collision.gameObject.CompareTag("EnemyAttack"))
        //    {
        //        Destroy(collision.gameObject);
        //    }
        //}
    }

    public IEnumerator WaitCoolDown(float duration) {
        yield return new WaitForSeconds(duration);
        onCooldown = false;
    }
    public void TakeDamage(float damage)
    {
        this.stats.TakeDamage(damage);
        //this.isInHitStun = true;
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

    private IEnumerator Shooting()
    {
        this.canShoot = false;
        yield return new WaitForSeconds(this.stats.fireRate);
        this.canShoot = true;
    }

    public void ExecuteEffect(IPlayerEffect effect)
    {
        effect.Execute(this.gameObject);
    }

    private IEnumerator IFrame()
    {
        this.isInIFrame = true;
        var playerSpriteRenderer = this.gameObject.GetComponent<SpriteRenderer>().color;
        var red = playerSpriteRenderer.r;
        var blue = playerSpriteRenderer.b;
        var green = playerSpriteRenderer.g;
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(red, blue, green, 0.5f);

        yield return new WaitForSeconds(this.iFrameTime);

        playerSpriteRenderer = this.gameObject.GetComponent<SpriteRenderer>().color;
        red = playerSpriteRenderer.r;
        blue = playerSpriteRenderer.b;
        green = playerSpriteRenderer.g;
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(red, blue, green, 1.0f);
        this.isInIFrame = false;
    }

    public bool IsInIFrame()
    {
        return this.isInIFrame;
    }

    public float GetIFrameTime()
    {
        return this.iFrameTime;
    }
    private IEnumerator Die()
    {
        if (!this.isDead)
        {
            var animator = this.gameObject.GetComponent<Animator>();
            animator.SetBool("Dead", true);
            FindObjectOfType<SoundManager>().PlaySoundEffect("Death");
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + 1);

            SceneManager.LoadScene(8);
            FindObjectOfType<SoundManager>().PlayMusicTrack("Game Over");
            
        }
    }

    public IEnumerator InvincibleForXSeconds(float duration)
    {
        // Check if there is any other iframe coroutine running, stop it and start invincibility one
        if(iframeRoutine != null) 
        {
            StopCoroutine(iframeRoutine);
        }

        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.5f);
        this.canDodge = false;
        this.isInIFrame = true;
        yield return new WaitForSeconds(3*duration/5);
        // Change slightly back to color, for indication its ending soon
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 0, 0.5f);
        yield return new WaitForSeconds(2*duration/5);
        this.canDodge = true;
        this.isInIFrame = false;
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1);
    }

    public PlayerStats GetStats()
    {
        var stats_copy = this.stats;
        return stats_copy;
    }

    public void ChangeRangedAttack(IPlayerCommand newAttack)
    {
        this.fire1 = newAttack;
    }

    public void ChangeMeleeAttack(IPlayerCommand newAttack)
    {
        this.fire2 = newAttack;
    }

    public void SetActiveSpell(IPlayerSpell spell, Sprite sprite)
    {
        this.activeSpell1 = spell;
        if(sprite == null)
        {
            this.playersCurrentItemBar.Updateslots(2,sprite);
        }else
        {
            this.playersCurrentItemBar.Updateslots(2,sprite);
        }

    }

    public string GetActiveSpell()
    {
        return activeSpell1.GetName();
    }

    public void InvertControls()
    {
        var left = this.left;
        var right = this.right;
        var up = this.up;
        var down = this.down;
        var upLeft = this.upLeft;
        var upRight = this.upRight;
        var downRight = this.downRight;
        var downLeft = this.downLeft;

        this.right = left;
        this.left = right;
        this.up = down;
        this.down = up;
        this.upLeft = downRight;
        this.upRight = downLeft;
        this.downRight = upLeft;
        this.downLeft = upRight;
    }

    public IEnumerator Dodge(float duration, Vector2 direction)
    {
        var animator = this.gameObject.GetComponent<Animator>();
        animator.SetBool("Rolling", true);
        this.isEnabled = false;
        this.canDodge = false;
        this.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.01f, 0.01f);
        this.isInIFrame = true;
        for(int i = 0; i < 32; i++)
        {
            this.gameObject.GetComponent<Rigidbody2D>().velocity = direction * this.stats.GetSpeed() * 2.5f;
            yield return new WaitForSeconds(duration/32);
        }

        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        this.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.24f, 0.3f);
        this.isInIFrame = false;
        this.isEnabled = true;
        animator.SetBool("Rolling", false);

        StartCoroutine(BufferDodge());
    }

    private IEnumerator BufferDodge()
    {
        yield return new WaitForSeconds(0.2f);
        this.canDodge = true;
    }

    public void ChangeWeapon(Weapon weapon)
    {
        this.stats.SetActiveWeapon(weapon);
        this.playersCurrentItemBar.Updateslots(0, this.stats.activeWeapon.GetComponent<Weapon>().sprite);
    }
}