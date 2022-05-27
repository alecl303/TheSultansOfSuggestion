using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour 
{
    bool isPaused = false;
    GameObject CanvasObject;
    GameObject PlayerObject;
    GameObject SoundManager;

    public GameObject DontDestroyOnLoad;
    
    public void Pause()
    {
        if (isPaused) 
        {
            Time.timeScale = 1;
            isPaused = false;
            PlayerObject.GetComponent<PlayerController> ().enabled = true;
            CanvasObject.GetComponent<Canvas> ().enabled = false;
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
            PlayerObject.GetComponent<PlayerController> ().enabled = false;
            CanvasObject.GetComponent<Canvas> ().enabled = true;
        }
    }

    void Awake()
    {
        CanvasObject = GameObject.Find("/Pause");
        PlayerObject = GameObject.Find("/Player_Object");
        
        print(CanvasObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Quit()
    {
        isPaused = false;
        Time.timeScale = 1;
        CanvasObject.GetComponent<Canvas> ().enabled = false;
        //Destroy(this.DontDestroyOnLoad);
        //Destroy(this.DontDestroyOnLoad);
        SceneManager.LoadScene(0);

        //Application.Quit();
    }
}