using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeleteOldGame : MonoBehaviour
{
    public GameObject DontDestroyOnLoad;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Death")
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            this.DontDestroyOnLoad.GetComponent<DontDestroyOnLoad>().DestroyAll();
        }
        
    }
}
