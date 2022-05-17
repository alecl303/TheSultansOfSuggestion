using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Effect;
public class EffectController : MonoBehaviour // Will probably have to change this. We will know better once the actual card setup is done
{
    public void AffectPlayer(IPlayerEffect effect)
    {
        FindObjectOfType<PlayerController>().ExecuteEffect(effect);
        Destroy(this.gameObject);
    }
       
}
