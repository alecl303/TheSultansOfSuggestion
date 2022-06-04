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
    private Sprite spellSprite;
    [SerializeField] private GameObject playerTarget;

    private bool doubleShotBanned = false;
    private bool invertControlsBanned = false;
    private bool bloodForSpellsBanned = false;
    private bool backwardsBulletBanned = false;
    private bool hasActiveSpell = false;

    void Awake(){
        Icon = GameObject.Find("Icon");
    }

    void OnEnable()
    {
        Time.timeScale = 0;
        playerTarget.GetComponent<PlayerController>().enabled = false;

        DetermineRandomWeapon();
        DetermineRandomSpell();
        InitializeDebuffs();
        InitializeBuff();
        InitializeCardText();
    }

    void InitializeDebuffs()
    {
        IPlayerEffect randomDebuff;
        for (int i = 0; i < 3; i++)
        {
            do
            {
                randomDebuff = debuffManager.GetComponent<DebuffManager>().GetRandomDebuff();
            } while (debuffList.Contains(randomDebuff) ||
            (randomDebuff.GetName() == "InvertControls" && invertControlsBanned) ||
            (randomDebuff.GetName() == "BackwardsBulletEffect" && backwardsBulletBanned) ||
            (randomDebuff.GetName() == "SpellsCostBlood" && bloodForSpellsBanned) ||
            (randomDebuff.GetName() == "LoseActiveSpell" && !hasActiveSpell) ||
            (randomDebuff.GetName() == "LoseActiveSpell" && hasActiveSpell && i == 2));

            debuffList.Add(randomDebuff);
        }
    }

    void InitializeBuff()
    {
        IPlayerEffect randomBuff;
        do
        {
            randomBuff = buffManager.GetComponent<BuffManager>().GetRandomBuff();
        } while (randomBuff.GetName() == "DoubleShot" && doubleShotBanned);
        buffList.Add(randomBuff);
        this.newWeapon =  this.weapon.GetComponent<Weapon>();
        this.newWeapon.randomize();
        this.newSprite = this.playerTarget.GetComponent<WeaponSprites>().sprites[this.newWeapon.spriteIndex];
        this.newWeapon.SetSprite(this.newSprite);

        Icon.GetComponent<Image>().sprite = this.newSprite;
        playerSpell = SpellManager.GetComponent<SpellManager>().GetRandomSpell();
        this.spellSprite= SpellManager.GetComponent<SpellManager>().GetSpellSprite();
    }

    void InitializeCardText()
    {
        Card1.GetComponent<TMPro.TextMeshProUGUI>().text = buffList[0].GetDescription() + "\n\n\n\n" + debuffList[0].GetDescription();
        Card2.GetComponent<TMPro.TextMeshProUGUI>().text = this.newWeapon.GetDescription() + "\n\n\n\n" + debuffList[1].GetDescription();
        Card3.GetComponent<TMPro.TextMeshProUGUI>().text = playerSpell.GetDescription() + "\n\n\n\n" + debuffList[2].GetDescription();
    }
    
    public void ApplyCard1()
    {
        buffList[0].Execute(playerTarget);
        debuffList[0].Execute(playerTarget);
        UpdateBans(true, 0);
        removeCardsFromScreen();
    }

    public void ApplyCard2()
    {
        this.playerTarget.GetComponent<PlayerController>().ChangeWeapon(this.newWeapon);
        debuffList[1].Execute(playerTarget);
        UpdateBans(false, 1);
        removeCardsFromScreen();
    }

    public void ApplyCard3()
    {

        this.playerTarget.GetComponent<PlayerController>().SetActiveSpell(this.playerSpell, this.spellSprite);
        debuffList[2].Execute(playerTarget);
        hasActiveSpell = true;
        UpdateBans(false, 2);
        removeCardsFromScreen();
    }

    void removeCardsFromScreen()
    {
        this.buffList.Clear();
        this.debuffList.Clear();
        this.transform.parent.gameObject.SetActive(false);
        
        Time.timeScale = 1;
        playerTarget.GetComponent<PlayerController>().enabled = true;
        // TODO: Reenable for different scenes
    }

    void UpdateBans(bool buffTaken, int debuffIndex)
    {
        if (buffTaken && buffList[0].GetName() == "DoubleShot")
        {
            doubleShotBanned = true;
        }

        if (debuffList[debuffIndex].GetName() == "InvertControls")
        {
            invertControlsBanned = true;
        }

        if (debuffList[debuffIndex].GetName() == "BackwardsBulletEffect")
        {
            backwardsBulletBanned = true;
        }

        if (debuffList[debuffIndex].GetName() == "SpellsCostBlood")
        {
            bloodForSpellsBanned = true;
        }
    }

    void DetermineRandomWeapon()
    {
        this.newWeapon = this.weapon.GetComponent<Weapon>();
        this.newWeapon.Randomize();
        this.newSprite = this.playerTarget.GetComponent<WeaponSprites>().sprites[this.newWeapon.spriteIndex];
        this.newWeapon.SetSprite(this.newSprite);
        Icon.GetComponent<Image>().sprite = this.newSprite;
    }

    void DetermineRandomSpell()
    {
        // To prevent players from getting the same spell they currently have. 
        IPlayerSpell spellPlaceholder;

        do
        {
            spellPlaceholder = SpellManager.GetComponent<SpellManager>().GetRandomSpell();
        } while (playerTarget.GetComponent<PlayerController>().GetActiveSpell() == spellPlaceholder.GetName());

        this.playerSpell = spellPlaceholder;
    }
}