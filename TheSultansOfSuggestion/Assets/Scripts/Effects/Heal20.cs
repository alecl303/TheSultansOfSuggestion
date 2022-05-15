using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal20 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<PlayerController>().Heal(20);
    }

}
