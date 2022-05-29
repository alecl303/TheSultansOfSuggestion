using UnityEngine;
using System.Collections;

public interface ISpellEffect
{
    IEnumerator ApplyEffect(PlayerController target);
}