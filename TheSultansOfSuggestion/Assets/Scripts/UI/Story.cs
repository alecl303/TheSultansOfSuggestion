using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Story : MonoBehaviour 
{


    public void DisableCanvas()
    {
        this.gameObject.GetComponent<Canvas>().enabled = false;
        FindObjectOfType<DontDestroyOnLoad>().SelectCard();
    }

    public void DisablePostChoiceCanvas()
    {
        this.gameObject.GetComponent<Canvas>().enabled = false;
        FindObjectOfType<DontDestroyOnLoad>().IncrementScene();
    }

}