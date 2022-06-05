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
        removeCardsFromScreen(0);
    }

    public void ApplyCard2()
    {
        this.playerTarget.GetComponent<PlayerController>().ChangeWeapon(this.newWeapon);
        debuffList[1].Execute(playerTarget);
        UpdateBans(false, 1);
        removeCardsFromScreen(1);
    }

    public void ApplyCard3()
    {
        this.playerTarget.GetComponent<PlayerController>().SetActiveSpell(this.playerSpell, this.spellSprite);
        debuffList[2].Execute(playerTarget);
        hasActiveSpell = true;
        UpdateBans(false, 2);
        removeCardsFromScreen(2);
    }

    void removeCardsFromScreen(int cardNumber)
    {
        string response = DeterminePostChoiceText(cardNumber);
        this.buffList.Clear();
        this.debuffList.Clear();
        this.transform.parent.gameObject.SetActive(false);
        FindObjectOfType<DontDestroyOnLoad>().EnablePostChoiceText(response);
        Time.timeScale = 1;
        playerTarget.GetComponent<PlayerController>().enabled = true;
        // TODO: Reenable for different scenes
    }

    void UpdateBans(bool buffTaken, int debuffIndex)
    {
        if (buffTaken && buffList[0].GetName() == "TripleShot")
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

    string DeterminePostChoiceText(int cardNumber)
    {
        List<string> possibleResponses = new List<string>();
        if (cardNumber == 0)
        {
            string buffName = buffList[cardNumber].GetName();
            if (buffName == "CritChanceUp" || buffName == "CritMultiplierUp")
            {
                possibleResponses.Add("Gambling on lucky strikes, our contestant chooses the dramatic flair of a critical hit!");
            }
            else if (buffName == "LifestealUp")
            {
                possibleResponses.Add("Our champion obtains the power of vampirism... bleed 'em dry!");
            }
            else if (buffName == "PoisonChanceUp" || buffName == "PoisonDamageUp" || buffName == "PoisonTimeUp" || buffName == "StunChanceUp")
            {
                possibleResponses.Add("Our hero opts for tactical combat. Enemies will be completely disoriented!");
            }
            else if (buffName == "DoubleShot" || buffName == "TripleShot")
            {
                possibleResponses.Add("A spread-shot weapon! Our contestant's efficiency will be multiplied!");
            }
            else if (buffName == "BulletSpeedUp" || buffName == "DamageUp" || buffName == "RangeUp" || buffName == "MovementSpeedUp" || buffName == "ManaUp" || buffName == "MaxHealthUp")
            {
                possibleResponses.Add("Our hero finds a hearty meal to increase their basic abilities!");
            }
        }

        if (cardNumber == 1)
        {
            possibleResponses.Add("Our contestant picks up a powerful new weapon! Slice 'em up, hero! " +
            "Remember, if you like what you see, replica weapons can be obtained through our online shop!");
        }

        if (cardNumber == 2)
        {
            possibleResponses.Add("Our hero obtains a powerful magic spell! The arcane strength of the earth is on their side. " +
            "Many thanks to our special effects team for bringing the magic to life.");
        }

        string debuffName = debuffList[cardNumber].GetName();
        if (debuffName == "CritChanceDown" || debuffName == "CritMultiplierDown")
        {
            possibleResponses.Add("Our contestant chooses to reject critical hits. No luck needed for our champion!");
        } 
        else if (debuffName == "LifestealDown")
        {
            possibleResponses.Add("Our champion bleeds as much as their enemies, walking a thin line between life and death! Exciting!");
        }
        else if (debuffName == "InvertControls")
        {
            possibleResponses.Add("Oh no! Our contestant stumbles into a dizzy trap! Get your bearings quick!");
        }
        else if (debuffName == "LoseActiveSpell")
        {
            possibleResponses.Add("Deciding to reject magic, our hero takes on enemies head-on!");
        }
        else if (debuffName == "SpellsCostBlood")
        {
            possibleResponses.Add("Blood magic... Pain to one's enemies at the cost of one's own life force. Dramatic!");
        }
        else if (debuffName == "BulletSpeedDown" || debuffName == "DamageDown" || debuffName == "RangeDown" || debuffName == "MovementSpeedDown" || debuffName == "ManaDown" || debuffName == "MaxHealthDown")
        {
            possibleResponses.Add("Our contestant seems a little sick... Their basic abilities are reduced. Hopefully some bloodshed will perk them back up!");
        }

        if (possibleResponses.Count == 0)
        {
            possibleResponses.Add("Our contestant continues onward in the hopes that they succeed! Ha! Like that will happen!");
        }

        int responseChoice = Random.Range(0, possibleResponses.Count);
        return possibleResponses[responseChoice];
    }
}