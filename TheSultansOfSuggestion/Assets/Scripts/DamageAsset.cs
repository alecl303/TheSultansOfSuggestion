using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAsset : MonoBehaviour
{
    private static Transform _instance;

    public static Transform instance
    {
        get
        {
            if(_instance == null) _instance = Instantiate(Resources.Load<Transform>("DamageNumbers"));
            return _instance;
        }
    }
    public Transform damageNumbers;
}
