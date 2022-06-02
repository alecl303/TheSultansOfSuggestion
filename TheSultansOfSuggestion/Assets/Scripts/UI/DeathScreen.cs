using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
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
}
