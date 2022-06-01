using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public GameObject DontDestroyOnLoad;

    void OnSceneLoaded()
    {
        DeleteOldGame();
    }
    public void ContinueGame()
    {
        // Load Atlantis Scene
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        // Return to MainMenu
        SceneManager.LoadScene(0);
    }

    private void DeleteOldGame()
    {
        this.DontDestroyOnLoad = GameObject.Find("/DontDestroyOnLoad");
        this.DontDestroyOnLoad.GetComponent<DontDestroyOnLoad>().DestroyAll();
        Destroy(this.DontDestroyOnLoad);
    }
}
