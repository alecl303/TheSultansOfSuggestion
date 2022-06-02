using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Command;
public class SpellManager : MonoBehaviour
{
    List<IPlayerSpell> spells;
    IPlayerSpell randomSpell;
    [SerializeField] string description;
    private int spellLength = 0;

    public IPlayerSpell GetRandomSpell()
    {
        if (this.spellLength == 0)
        {
            InitDebuffs();
        }
        return (this.spells[Random.Range(0, this.spellLength)]);
    }

    void InitDebuffs()
    {
        this.spells = new List<IPlayerSpell>() {
            ScriptableObject.CreateInstance<Berserk>(),
            ScriptableObject.CreateInstance<Burst>(),
            ScriptableObject.CreateInstance<FreezeEnemies>(),
            ScriptableObject.CreateInstance<Invincibility>(),
            ScriptableObject.CreateInstance<Whirlwind>(),
        };
        this.spellLength = this.spells.Count;
    }
}
