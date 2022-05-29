using UnityEngine;
using System.Collections;
using Player.Stats;

public class IcicleEffect : MonoBehaviour, IEnemyTrapSpellEffect
{
    // [SerializeField] private GameObject target;
    [SerializeField] private float spellDamage;
    [SerializeField] private GameObject bulletPrefab;
    private bool overlap = false;
    private bool repeat = true;
    // private PlayerStats target;

    public void SetOverlap(bool overlap)
    {
        this.overlap = overlap;
    }
    public IEnumerator ApplyEffect(EnemyController target, PlayerStats playerStats)
    {

        // this.target.GetComponent<PlayerController>().GetStats().Heal(flatHeal*statsMultiplier);
        // Debug.Log("Healing");
        // playerStats.Heal(flatHeal*playerStats.SpellStrength);

        var rigidBody = this.GetComponent<Rigidbody2D>();
        Debug.Log(rigidBody.transform.position.x + " y " + rigidBody.transform.position.y);
        while (repeat)
        {
            for (int i = 0; i < 360; i += 15)
            {
                float theta = i * (Mathf.PI / 180);

                var r = Mathf.Sin(1.3f * i);

                var targetPos = new Vector2(rigidBody.transform.position.x + (r * Mathf.Cos(theta)), rigidBody.transform.position.y + (r * Mathf.Sin(theta)));
                
                var x = rigidBody.transform.position.x;
                var y = rigidBody.transform.position.y;
                var bulletController = Instantiate(bulletPrefab, new Vector3(x, y, rigidBody.transform.position.z), Quaternion.Euler(0.0f, 0.0f, i)).gameObject.GetComponent<PlayerBulletController>();

                bulletController.SetTarget(targetPos);
                bulletController.SetDamage(playerStats.GetRangeDamage());
                bulletController.SetBulletSpeed(playerStats.GetBulletSpeed());
                
                yield return new WaitForSeconds(0.5f);
            }
        }

        
        Debug.Log("Finished applying effect");
    }
    void OnDestroy()
    {
        repeat = false;      
    }
}
