using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Player.Effect;
using Player.Command;
using Player.Stats;
public class CardSelectionController : MonoBehaviour 
{
    public GameObject buffManager;
    public GameObject debuffManager;
    public GameObject SpellManager;
    private List<IPlayerEffect> buffList = new List<IPlayerEffect>();
    private List<IPlayerEffect> debuffList = new List<IPlayerEffect>();
    private GameObject newSpell;
    private IPlayerCommand playerSpell;
    public GameObject Card1;
    public GameObject Card2;
    public GameObject Card3;

    [SerializeField] private GameObject playerTarget;

    void OnEnable()
    {
        Time.timeScale = 0;
        IPlayerEffect randomBuff;
        IPlayerEffect randomDebuff;
        // this.GetComponentInParent<Canvas>().enabled = true;
        playerTarget.GetComponent<PlayerController> ().enabled = false;

        for (int i = 0; i < 2; i++)
        {
            do {
                randomBuff = buffManager.GetComponent<BuffManager>().GetRandomBuff();
            } while (buffList.Contains(randomBuff));

            do {
                randomDebuff = debuffManager.GetComponent<DebuffManager>().GetRandomDebuff();
            } while (debuffList.Contains(randomDebuff));

            buffList.Add(randomBuff);
            debuffList.Add( randomDebuff);

        }

        playerSpell = SpellManager.GetComponent<SpellManager>().GetRandomSpell();
        Card1.GetComponent<TMPro.TextMeshProUGUI>().text = buffList[0].GetDescription() + "\n\n\n\n" + debuffList[0].GetDescription();
        Card2.GetComponent<TMPro.TextMeshProUGUI>().text = buffList[1].GetDescription() + "\n\n\n\n" + debuffList[1].GetDescription();
        Card3.GetComponent<TMPro.TextMeshProUGUI>().text = playerSpell + "\n\n\n\n exchange for a current random spell";

        //AssignButtons();
    }

    
    
    public void ApplyCard1()
    {

        buffList[0].Execute(playerTarget);
        debuffList[0].Execute(playerTarget);
        removeCardsFromScreen();
    
    }

    public void ApplyCard2()
    {

        buffList[1].Execute(playerTarget);
        debuffList[1].Execute(playerTarget);
        removeCardsFromScreen();
    }

    public void ApplyCard3()
    {
        //this.playerTarget.GetComponent<PlayerStats>().activeSpell = this.newSpell;

        this.playerTarget.GetComponent<PlayerController>().ChangeSpellAttack(this.playerSpell);
        removeCardsFromScreen();
    }

    // void AssignButtons()
    // {
    //     Card1.GetComponentInParent<Button>().onClick.AddListener(ApplyCard1);
    //     Card2.GetComponentInParent<Button>().onClick.AddListener(ApplyCard2);
    //     Card3.GetComponentInParent<Button>().onClick.AddListener(ApplyCard3);
    // }

    void removeCardsFromScreen()
    {
        //this.GetComponentInParent<Canvas>().enabled = false;
        this.buffList.Clear();
        this.debuffList.Clear();

        this.transform.parent.gameObject.SetActive(false);
        
        Time.timeScale = 1;
        playerTarget.GetComponent<PlayerController> ().enabled = true;
        // TODO: Reenable for different scenes
    }

}