using UnityEngine;
using System.Collections;
using Player.Stats;

public interface IEnemyTrapSpellEffect
{
    void SetOverlap(bool overlap);
    IEnumerator ApplyEffect(EnemyController target, PlayerStats playerStats);
}