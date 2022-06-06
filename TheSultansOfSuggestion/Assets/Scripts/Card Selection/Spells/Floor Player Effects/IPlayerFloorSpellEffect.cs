using UnityEngine;
using System.Collections;

public interface IPlayerFloorSpellEffect
{
    void SetOverlap(bool overlap);
    void ApplyEffect(PlayerController target);
}