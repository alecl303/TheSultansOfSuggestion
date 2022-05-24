using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAmount
{
    public static float GetRandomAmount()
    {
        return Random.Range(0.2f, 0.6f);
    }
}
