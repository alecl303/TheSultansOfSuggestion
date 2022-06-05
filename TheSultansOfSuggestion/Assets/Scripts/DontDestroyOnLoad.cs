using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoad : MonoBehaviour
{
    int scene = 1;
    public GameObject player;
    //public GameObject soundManager;
    public GameObject pauseMenu;
    public GameObject HUD;
    public GameObject currentCamera;
    public GameObject eventSystem;
    public GameObject cardSelection;
    public GameObject death;
    public GameObject damageNumbers;

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
        DontDestroyOnLoad(FindObjectOfType<SoundManager>().gameObject);
        DontDestroyOnLoad(this.pauseMenu);
        DontDestroyOnLoad(this.HUD);
        DontDestroyOnLoad(this.currentCamera);
        DontDestroyOnLoad(this.eventSystem);
        DontDestroyOnLoad(this.cardSelection);
        DontDestroyOnLoad(this.death);
        DontDestroyOnLoad(this.damageNumbers);
        scene++;

    }

    public void SelectCard()
    {
        this.cardSelection.SetActive(true);
    }

    public void EnableText()
    {
        var Story = GameObject.Find("/Story");
        Story.GetComponent<Canvas>().enabled = true;
    }

    public void EnablePostChoiceText(string response)
    {
        var PostChoiceText = GameObject.Find("/PostCardChoiceText");
        PostChoiceText.GetComponentInChildren<UnityEngine.UI.Text>().text = response;
        PostChoiceText.GetComponent<Canvas>().enabled = true;
    }

    public void EnableStartingText()
    {
        var OnEntranceText = GameObject.Find("/OnEntranceText");
        Time.timeScale = 0;
        player.GetComponent<PlayerController>().enabled = false;
        OnEntranceText.GetComponent<Canvas>().enabled = true;
    }

    public void DestroyAll()
    {
        Destroy(this.gameObject);
        Destroy(this.player);
        Destroy(FindObjectOfType<SoundManager>().gameObject);
        Destroy(this.pauseMenu);
        Destroy(this.HUD);
        Destroy(this.currentCamera);
        Destroy(this.eventSystem);
        Destroy(this.cardSelection);
        Destroy(this.death);
        Destroy(this.damageNumbers);
    }
}