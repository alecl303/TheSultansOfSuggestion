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
    GameObject HUD;

    public GameObject DontDestroyOnLoad;
    
    public void Pause()
    {
        if (isPaused) 
        {
            Time.timeScale = 1;
            isPaused = false;
            PlayerObject.GetComponent<PlayerController> ().enabled = true;
            CanvasObject.GetComponent<Canvas> ().enabled = false;
            HUD.GetComponent<Canvas> ().enabled = true;
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
            PlayerObject.GetComponent<PlayerController> ().enabled = false;
            CanvasObject.GetComponent<Canvas> ().enabled = true;
            HUD.GetComponent<Canvas> ().enabled = false;
        }
    }

    void Awake()
    {
        CanvasObject = GameObject.Find("/Pause");
        PlayerObject = GameObject.Find("/Player_Object");
        HUD= GameObject.Find("/HUD");
        print(CanvasObject);
    }

    void Update()
    {
        if (Input.GetButtonDown("Menu"))
        {
            Pause();
        }
    }

    public void Quit()
    {
        isPaused = false;
        Time.timeScale = 1;
        CanvasObject.GetComponent<Canvas> ().enabled = false;
        this.DontDestroyOnLoad.GetComponent<DontDestroyOnLoad>().DestroyAll();
        Destroy(this.DontDestroyOnLoad);
        //Destroy(this.DontDestroyOnLoad);
        SceneManager.LoadScene(0);
        

        //Application.Quit();
    }
}