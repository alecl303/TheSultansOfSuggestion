using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    void Start()
    {
        FindObjectOfType<PlayerController>().GetComponent<Transform>().position = this.gameObject.GetComponent<Transform>().position;
    }

}
