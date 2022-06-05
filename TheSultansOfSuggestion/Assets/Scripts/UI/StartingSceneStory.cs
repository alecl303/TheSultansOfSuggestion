using System.Collections;
using UnityEngine;


public class StartingSceneStory : MonoBehaviour
{
    private GameObject playerTarget;

    private void Awake()
    {
        Time.timeScale = 0;
        playerTarget = GameObject.Find("/Player_Object");
        playerTarget.GetComponent<PlayerController>().enabled = false;
    }
    public void DisableStartingCanvas()
    {
        this.gameObject.GetComponent<Canvas>().enabled = false;
        Time.timeScale = 1;
        playerTarget.GetComponent<PlayerController>().enabled = true;
    }
}
