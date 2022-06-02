using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player.Command;
using Player.Effect;
public class SpellManager : MonoBehaviour
{

    private List<IPlayerCommand> spellsScriptable;
    private int spellLength=0;


    

    public IPlayerCommand GetRandomSpell()
    {
        if(this.spellLength == 0)
        {
            initSpells();
        }
        return this.spellsScriptable[UnityEngine.Random.Range(0, spellLength)];
    }

    void initSpells(){
        this.spellsScriptable = new List<IPlayerCommand>() {
            ScriptableObject.CreateInstance<Whirlwind>(),
            ScriptableObject.CreateInstance<FreezeEnemies>(),
        };
        this.spellLength = this.spellsScriptable.Count;
    }
}