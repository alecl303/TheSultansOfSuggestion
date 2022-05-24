using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoad : MonoBehaviour
{

    int scene = 1;
    public GameObject player;
    public GameObject soundManager;
    public GameObject pauseMenu;
    public GameObject HUD;
    public GameObject currentCamera;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (scene == 1)
            {
                SceneManager.LoadScene("Caves Room");
                DontDestroyOnLoad(this.gameObject);
                DontDestroyOnLoad(this.player);
                DontDestroyOnLoad(this.soundManager);
                DontDestroyOnLoad(this.pauseMenu);
                DontDestroyOnLoad(this.HUD);
                DontDestroyOnLoad(this.currentCamera);
                scene++;
            }
            else
            {
                SceneManager.LoadScene("TestScene");
                DontDestroyOnLoad(this.gameObject);
                DontDestroyOnLoad(this.player);
                DontDestroyOnLoad(this.soundManager);
                DontDestroyOnLoad(this.pauseMenu);
                DontDestroyOnLoad(this.HUD);
                DontDestroyOnLoad(this.currentCamera);
                scene--;
            }
        }
    }
}