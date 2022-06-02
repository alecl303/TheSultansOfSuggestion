using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.Command;
using Player.Effect;
public class SpellManager : MonoBehaviour
{

    private List<IPlayerSpell> spellsScriptable;
    private int spellLength=0;


    

    public IPlayerSpell GetRandomSpell()
    {
        if(this.spellLength == 0)
        {
            initSpells();
        }
        return this.spellsScriptable[UnityEngine.Random.Range(0, spellLength)];
    }

    void initSpells(){
        this.spellsScriptable = new List<IPlayerSpell>() {
            ScriptableObject.CreateInstance<Whirlwind>(),
            ScriptableObject.CreateInstance<FreezeEnemies>(),
            ScriptableObject.CreateInstance<Invincibility>(),
            ScriptableObject.CreateInstance<Berserk>(),
        };
        this.spellLength = this.spellsScriptable.Count;
    }
}