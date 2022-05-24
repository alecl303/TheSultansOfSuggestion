using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CardSelectionController : MonoBehaviour 
{
    public Buff [] buffList;
    public Debuff [] debuffList;

    public GameObject Card1;
    public GameObject Card2;
    public GameObject Card3;

    void Start()
    {
        Time.timeScale = 0;
        GameObject.Find("/Player_Object").GetComponent<PlayerController> ().enabled = false;
        Card1.GetComponent<TMPro.TextMeshProUGUI>().text = buffList[Random.Range(0, buffList.Length)].description + "\n\n\n\n" + debuffList[Random.Range(0, debuffList.Length)].description;
        Card2.GetComponent<TMPro.TextMeshProUGUI>().text = buffList[Random.Range(0, buffList.Length)].description + "\n\n\n\n" + debuffList[Random.Range(0, debuffList.Length)].description;
        Card3.GetComponent<TMPro.TextMeshProUGUI>().text = buffList[Random.Range(0, buffList.Length)].description + "\n\n\n\n" + debuffList[Random.Range(0, debuffList.Length)].description;
    }
    
    void ApplyCard()
    {

    }
    // public void Pause()
    // {
    //     if (isPaused) 
    //     {
    //         Time.timeScale = 1;
    //         isPaused = false;
    //         PlayerObject.GetComponent<PlayerController> ().enabled = true;
    //         CanvasObject.GetComponent<Canvas> ().enabled = false;
    //     }
    //     else
    //     {
    //         Time.timeScale = 0;
    //         isPaused = true;
    //         PlayerObject.GetComponent<PlayerController> ().enabled = false;
    //         CanvasObject.GetComponent<Canvas> ().enabled = true;
    //     }
    // }

    // void Awake()
    // {
    //     CanvasObject = GameObject.Find("/Pause");
    //     PlayerObject = GameObject.Find("/Player_Object");
        
    //     print(CanvasObject);
    // }

    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Escape))
    //     {
    //         Pause();
    //     }
    // }

    // public void Quit()
    // {
    //     isPaused = false;
    //     Time.timeScale = 1;
    //     SceneManager.LoadScene(0);
    // }
}