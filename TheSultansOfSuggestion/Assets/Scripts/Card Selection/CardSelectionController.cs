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
    public GameObject weapon;
    [SerializeField] private GameObject Icon;
    private List<IPlayerEffect> buffList = new List<IPlayerEffect>();
    private List<IPlayerEffect> debuffList = new List<IPlayerEffect>();
    private IPlayerSpell playerSpell;
    private Weapon newWeapon;
    public GameObject Card1;
    public GameObject Card2;
    public GameObject Card3;
    private Sprite newSprite;
    [SerializeField] private GameObject playerTarget;

    void Awake(){
        Icon = GameObject.Find("Icon");
    }
    void OnEnable()
    {
        Time.timeScale = 0;
        IPlayerEffect randomBuff;
        IPlayerEffect randomDebuff;
        // this.GetComponentInParent<Canvas>().enabled = true;
        playerTarget.GetComponent<PlayerController>().enabled = false;

        for (int i = 0; i < 3; i++)
        {
            do {
                randomDebuff = debuffManager.GetComponent<DebuffManager>().GetRandomDebuff();
            } while (debuffList.Contains(randomDebuff));

            debuffList.Add(randomDebuff);
        }

        do {
                randomBuff = buffManager.GetComponent<BuffManager>().GetRandomBuff();
        } while (buffList.Contains(randomBuff));

        buffList.Add(randomBuff);
        this.newWeapon =  this.weapon.GetComponent<Weapon>();
        this.newWeapon.Randomize();
        this.newSprite = this.playerTarget.GetComponent<WeaponSprites>().sprites[this.newWeapon.spriteIndex];
        this.newWeapon.SetSprite(this.newSprite);
        Icon.GetComponent<Image>().sprite = this.newSprite;
        playerSpell = DetermineRandomSpell();
        Card1.GetComponent<TMPro.TextMeshProUGUI>().text = buffList[0].GetDescription() + "\n\n\n\n" + debuffList[0].GetDescription();
        Card2.GetComponent<TMPro.TextMeshProUGUI>().text = this.newWeapon.GetDescription() + "\n\n\n\n" + debuffList[1].GetDescription();
        Card3.GetComponent<TMPro.TextMeshProUGUI>().text = playerSpell.GetDescription() + "\n\n\n\n" + debuffList[2].GetDescription();

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
        this.playerTarget.GetComponent<PlayerController>().ChangeWeapon(this.newWeapon);
        debuffList[1].Execute(playerTarget);
        removeCardsFromScreen();
    }

    public void ApplyCard3()
    {
        this.playerTarget.GetComponent<PlayerController>().SetActiveSpell(this.playerSpell);
        debuffList[2].Execute(playerTarget);
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
        playerTarget.GetComponent<PlayerController>().enabled = true;
        // TODO: Reenable for different scenes
    }

    IPlayerSpell DetermineRandomSpell()
    {
        // To prevent players from getting the same spell they currently have. 
        IPlayerSpell spellPlaceholder;

        do
        {
            spellPlaceholder = SpellManager.GetComponent<SpellManager>().GetRandomSpell();
        } while (playerTarget.GetComponent<PlayerController>().GetActiveSpell() == spellPlaceholder.GetName());

        return spellPlaceholder;
    }
}