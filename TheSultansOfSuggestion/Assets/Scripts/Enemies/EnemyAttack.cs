using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class EnemyAttack : MonoBehaviour
{
    protected int damage = 2;
    public int GetDamage()
    {
        return this.damage;
    }
}