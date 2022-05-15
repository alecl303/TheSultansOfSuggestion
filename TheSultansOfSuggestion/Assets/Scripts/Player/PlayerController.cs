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

    [SerializeField] private float hitStunTimer = 0;
    [SerializeField] private float hitStunTime = 0.3f;
    [SerializeField] private bool isInHitStun = false;
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public GameObject hitboxPrefab;

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
        

    }

    // Update is called once per frame
    void Update()
    {
        if (!isInHitStun)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                this.fire1.Execute(this.gameObject);
            }
            if (Input.GetButtonDown("Fire2"))
            {
                this.fire2.Execute(this.gameObject);
            }
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

            var animator = this.gameObject.GetComponent<Animator>();
            animator.SetFloat("Velocity", Mathf.Max(Mathf.Abs(this.gameObject.GetComponent<Rigidbody2D>().velocity.x), Mathf.Abs(this.gameObject.GetComponent<Rigidbody2D>().velocity.y)));
        }
        else
        {
            HitStun();
        }  
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            var playerRigidBody = this.gameObject.GetComponent<Rigidbody2D>();
            var enemy = collision.gameObject.GetComponent<EnemyController>();

            playerRigidBody.velocity = (enemy.GetKnockback() * (playerRigidBody.position - collision.gameObject.GetComponent<Rigidbody2D>().position).normalized);

            TakeDamage(enemy.GetAttackDamage());
        }

        if (collision.gameObject.CompareTag("Bullet"))
        {
            var playerRigidBody = this.gameObject.GetComponent<Rigidbody2D>();
            var bulletKnockBack = 10; // temporary

            playerRigidBody.velocity = (bulletKnockBack * (playerRigidBody.position - collision.gameObject.GetComponent<Rigidbody2D>().position).normalized);

            TakeDamage(collision.gameObject.GetComponent<BulletController>().GetBulletDamage());
            Destroy(collision.gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            var playerRigidBody = this.gameObject.GetComponent<Rigidbody2D>();
            var bulletKnockBack = 10; // temporary

            playerRigidBody.velocity = (bulletKnockBack * (playerRigidBody.position - other.gameObject.GetComponent<Rigidbody2D>().position).normalized);

            TakeDamage(other.gameObject.GetComponent<BulletController>().GetBulletDamage());
            Destroy(other.gameObject);
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

    private void HitStun()
    {
        if(this.hitStunTimer < this.hitStunTime)
        {
            this.hitStunTimer += Time.deltaTime;
        }
        else
        {
            this.hitStunTimer = 0;
            this.isInHitStun = false;
        }
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
        return this.meleeDamage;
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
}