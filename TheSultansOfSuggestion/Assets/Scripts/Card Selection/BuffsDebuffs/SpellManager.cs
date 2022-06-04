using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.Command;
using Player.Effect;
public class SpellManager : MonoBehaviour
{

    private List<IPlayerSpell> spellsScriptable;
    private int spellLength=0;
    public int spellsprite=0;
    [SerializeField]private List<Sprite> listOfSprites;
    

    public IPlayerSpell GetRandomSpell()
    {
        if(this.spellLength == 0)
        {
            initSpells();
        }
        this.spellsprite=UnityEngine.Random.Range(0, spellLength);
        return this.spellsScriptable[spellsprite];
    }

    public Sprite GetSpellSprite()
    {
        return this.listOfSprites[this.spellsprite];
    }
    void initSpells(){
        this.spellsScriptable = new List<IPlayerSpell>() {
            ScriptableObject.CreateInstance<Whirlwind>(),
            ScriptableObject.CreateInstance<FreezeEnemies>(),
            ScriptableObject.CreateInstance<Invincibility>(),
            ScriptableObject.CreateInstance<Berserk>(),
            ScriptableObject.CreateInstance<Burst>()
        };
        this.spellLength = this.spellsScriptable.Count;
    }
}