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
    private List<int> debuffIndices;
    private int buffIndex;

    void Start(){
        Icon = GameObject.Find("Icon");
        buffList = buffManager.GetComponent<BuffManager>().GetList();
        debuffList = debuffManager.GetComponent<DebuffManager>().GetList();
    }

    void OnEnable()
    {
        Time.timeScale = 0;
        //IPlayerEffect randomBuff;
        //IPlayerEffect randomDebuff;
        // this.GetComponentInParent<Canvas>().enabled = true;
        playerTarget.GetComponent<PlayerController>().enabled = false;

        //this.newWeapon = this.weapon.GetComponent<Weapon>();
        //this.newWeapon.Randomize();
        //this.newSprite = this.playerTarget.GetComponent<WeaponSprites>().sprites[this.newWeapon.spriteIndex];
        //this.newWeapon.SetSprite(this.newSprite);
        //Icon.GetComponent<Image>().sprite = this.newSprite;
        DetermineRandomWeapon();
        playerSpell = DetermineRandomSpell();
        //Generate 3 random debuff indices and 1 buff index
        //Do the spell check

        //for (int i = 0; i < 3; i++)
        //{
        //    do {
        //        randomDebuff = debuffManager.GetComponent<DebuffManager>().GetRandomDebuff();
        //    } while (debuffList.Contains(randomDebuff));

        //    debuffList.Add(randomDebuff);
        //}

        //Ensures uniqueness
        for (int i = 0; i < 3; i++)
        {
            int index = 0;
            do
            {
                index = GetRandomEffectIndex(debuffList);
            } while (debuffIndices.Contains(index));
            debuffIndices.Add(index);
        }
        buffIndex = GetRandomEffectIndex(buffList);
        EnsureCantLoseNoSpell();
        //do {
        //        randomBuff = buffManager.GetComponent<BuffManager>().GetRandomBuff();
        //} while (buffList.Contains(randomBuff));

        //buffList.Add(randomBuff);
        
        //Card1.GetComponent<TMPro.TextMeshProUGUI>().text = buffList[0].GetDescription() + "\n\n\n\n" + debuffList[0].GetDescription();
        //Card2.GetComponent<TMPro.TextMeshProUGUI>().text = this.newWeapon.GetDescription() + "\n\n\n\n" + debuffList[1].GetDescription();
        //Card3.GetComponent<TMPro.TextMeshProUGUI>().text = playerSpell.GetDescription() + "\n\n\n\n" + debuffList[2].GetDescription();

        Card1.GetComponent<TMPro.TextMeshProUGUI>().text = buffList[buffIndex].GetDescription() + "\n\n\n\n" + debuffList[debuffIndices[0]].GetDescription();
        Card2.GetComponent<TMPro.TextMeshProUGUI>().text = this.newWeapon.GetDescription() + "\n\n\n\n" + debuffList[debuffIndices[1]].GetDescription();
        Card3.GetComponent<TMPro.TextMeshProUGUI>().text = playerSpell.GetDescription() + "\n\n\n\n" + debuffList[debuffIndices[2]].GetDescription();

        //AssignButtons();
    }

    
    
    public void ApplyCard1()
    {
        buffList[buffIndex].Execute(playerTarget);
        debuffList[debuffIndices[0]].Execute(playerTarget);
        removeCardsFromScreen();
    }

    public void ApplyCard2()
    {
        this.playerTarget.GetComponent<PlayerController>().ChangeWeapon(this.newWeapon);
        debuffList[debuffIndices[1]].Execute(playerTarget);
        removeCardsFromScreen();
    }

    public void ApplyCard3()
    {
        this.playerTarget.GetComponent<PlayerController>().SetActiveSpell(this.playerSpell);
        debuffList[debuffIndices[2]].Execute(playerTarget);
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
        //this.buffList.Clear();
        //this.debuffList.Clear();
        CleanLists(debuffIndices, buffIndex);
        this.transform.parent.gameObject.SetActive(false);
        
        Time.timeScale = 1;
        playerTarget.GetComponent<PlayerController>().enabled = true;
        // TODO: Reenable for different scenes
    }

    void DetermineRandomWeapon()
    {
        this.newWeapon = this.weapon.GetComponent<Weapon>();
        this.newWeapon.Randomize();
        this.newSprite = this.playerTarget.GetComponent<WeaponSprites>().sprites[this.newWeapon.spriteIndex];
        this.newWeapon.SetSprite(this.newSprite);
        Icon.GetComponent<Image>().sprite = this.newSprite;
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

    int GetRandomEffectIndex(List<IPlayerEffect> effectList)
    {
        return Random.Range(0, effectList.Count);
    }

    // Post
    void CleanLists(List<int> debuffIndices, int buffIndex)
    {
        List<string> permaDebuffs = ["InvertControls", "BackwardsBulletEffect", "SpellsCostBlood"];
        List<IPlayerEffect> debuffsToRemove = new List<IPlayerEffect>();

        if (buffList[buffIndex].GetName() == "TripleShot")
        {
            buffList.ForEach((buff) =>
            {
                if (buff.GetName() == "DoubleShot")
                {
                    buffList.Remove(buff);
                }
            });
        }

        debuffIndices.ForEach((index) =>
        {
            if (permaDebuffs.Contains(debuffList[index].GetName()))
            {
                debuffsToRemove.Add(debuffList[index]);
            }
        });

        debuffsToRemove.ForEach((debuff) =>
        {
            debuffList.Remove(debuff);
        });
    }

    // Pre
    void EnsureCantLoseNoSpell()
    {
        int counter = 0;
        debuffIndices.ForEach((index) =>
        {
            if (debuffList[index].GetName() == "LoseActiveSpell" && playerTarget.GetComponent<PlayerController>().GetActiveSpell() == "Nothing")
            {
                int newIndex = 0;
                do
                {
                    newIndex = GetRandomEffectIndex(debuffList);
                } while (debuffList[newIndex].GetName() == "LoseActiveSpell" || debuffIndices.Contains(newIndex);
                debuffIndices[counter] = newIndex;
                break;
            }
            counter++;
        });
    }
}