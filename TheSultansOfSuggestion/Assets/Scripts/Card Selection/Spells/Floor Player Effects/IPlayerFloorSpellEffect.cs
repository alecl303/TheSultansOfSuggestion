using UnityEngine;
using System.Collections;

public interface IPlayerFloorSpellEffect
{
    void SetOverlap(bool overlap);
    IEnumerator ApplyEffect(PlayerController target);
}