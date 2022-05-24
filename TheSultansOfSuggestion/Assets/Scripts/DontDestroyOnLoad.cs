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

    public void IncrementScene()
    {
        switch (scene)
        {
            case 1:
                SceneManager.LoadScene("Caves Room");
                break;
            case 2:
                SceneManager.LoadScene("Dungeon Room");
                break;
            case 3:
                SceneManager.LoadScene("Caves Room 2");
                break;
            case 4:
                SceneManager.LoadScene("Boss Room");
                break;
        }

        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(this.player);
        DontDestroyOnLoad(this.soundManager);
        DontDestroyOnLoad(this.pauseMenu);
        DontDestroyOnLoad(this.HUD);
        DontDestroyOnLoad(this.currentCamera);
        scene++;
    }
}