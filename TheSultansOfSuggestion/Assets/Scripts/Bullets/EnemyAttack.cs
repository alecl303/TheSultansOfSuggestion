using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class EnemyAttack : MonoBehaviour
{
    protected float damage = 2;
    public float GetDamage()
    {
        return this.damage;
    }

    /*   Might use later
    public IEnumerator AnimateCollision()
    {
        var animator = this.gameObject.GetComponent<Animator>();
        animator.SetBool("Collided", true);

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + 0.3f);

        Destroy(this.gameObject);
    }
    */ 
}
