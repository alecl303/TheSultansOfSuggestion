using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Effect;
public class EffectController : MonoBehaviour // Will probably have to change this. We will know better once the actual card setup is done
{
    public void AffectPlayer(IPlayerEffect effect)
    {
        effect.Execute(this.gameObject);
    }


    // This is purely for testing ///////////////////////////////////////////////////////////////////////
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            IPlayerEffect test_effect = ScriptableObject.CreateInstance<BackwardsBulletEffect>(); // Test effect
            AffectPlayer(test_effect);
            Debug.Log(test_effect.GetDescription());
        }
    }
    /////////////////////////////////////////////////////////////////////////////////////////////////////
}
