using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Player.Effect;

public class CardSelectionController : MonoBehaviour 
{
    public GameObject buffManager;
    public GameObject debuffManager;
    private List<IPlayerEffect> buffList = new List<IPlayerEffect>();
    private List<IPlayerEffect> debuffList = new List<IPlayerEffect>();

    public GameObject Card1;
    public GameObject Card2;
    public GameObject Card3;

    [SerializeField] private GameObject playerTarget;

    void OnEnable()
    {
        Time.timeScale = 0;
        // this.GetComponentInParent<Canvas>().enabled = true;
        playerTarget.GetComponent<PlayerController> ().enabled = false;

        for (int i = 0; i < 3; i++)
        {

            buffList.Add(buffManager.GetComponent<BuffManager>().GetRandomBuff());
            debuffList.Add(debuffManager.GetComponent<DebuffManager>().GetRandomDebuff());
        }
        
        Card1.GetComponent<TMPro.TextMeshProUGUI>().text = buffList[0].GetDescription() + "\n\n\n\n" + debuffList[0].GetDescription();
        Card2.GetComponent<TMPro.TextMeshProUGUI>().text = buffList[1].GetDescription() + "\n\n\n\n" + debuffList[1].GetDescription();
        Card3.GetComponent<TMPro.TextMeshProUGUI>().text = buffList[2].GetDescription() + "\n\n\n\n" + debuffList[2].GetDescription();

        AssignButtons();
    }

    
    
    void ApplyCard1()
    {
        buffList[0].Execute(playerTarget);
        debuffList[0].Execute(playerTarget);
        removeCardsFromScreen();
    
    }

    void ApplyCard2()
    {
        buffList[1].Execute(playerTarget);
        debuffList[1].Execute(playerTarget);
        removeCardsFromScreen();
    }

    void ApplyCard3()
    {
        buffList[2].Execute(playerTarget);
        debuffList[2].Execute(playerTarget);
        removeCardsFromScreen();
    }

    void AssignButtons()
    {
        Card1.GetComponentInParent<Button>().onClick.AddListener(ApplyCard1);
        Card2.GetComponentInParent<Button>().onClick.AddListener(ApplyCard2);
        Card3.GetComponentInParent<Button>().onClick.AddListener(ApplyCard3);
    }

    void removeCardsFromScreen()
    {
        //this.GetComponentInParent<Canvas>().enabled = false;
        Time.timeScale = 1;
        playerTarget.GetComponent<PlayerController> ().enabled = true;

        this.buffList.Clear();
        this.debuffList.Clear();

        this.transform.parent.gameObject.SetActive(false);
        // TODO: Reenable for different scenes
    }

}