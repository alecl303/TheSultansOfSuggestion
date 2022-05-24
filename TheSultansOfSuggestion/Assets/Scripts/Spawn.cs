using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<PlayerController>().GetComponent<Transform>().position = this.gameObject.GetComponent<Transform>().position;
    }

}
