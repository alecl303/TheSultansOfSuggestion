using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Effect;
public class DebuffManager : MonoBehaviour
{
    List<IPlayerEffect> debuffs;
    [SerializeField] string description;
    private int debuffLength = 0;

    public IPlayerEffect GetRandomDebuff()
    {
        if (this.debuffLength == 0)
        {
            initDebuffs();
        }
        return (this.debuffs[Random.Range(0, this.debuffLength)]);
    }

    void initDebuffs()
    {
        this.debuffs = new List<IPlayerEffect>() {
            ScriptableObject.CreateInstance<BulletSpeedDown>(),
            ScriptableObject.CreateInstance<CritChanceDown>(),
            ScriptableObject.CreateInstance<CritMultiplierDown>(),
            ScriptableObject.CreateInstance<DamageDown>(),
            ScriptableObject.CreateInstance<BackwardsBulletEffect>(),
            //ScriptableObject.CreateInstance<DropTo1HP>(),
            ScriptableObject.CreateInstance<InvertControls>(),
            ScriptableObject.CreateInstance<LoseActiveSpell>(),
            //ScriptableObject.CreateInstance<LoseRoll>(),
            ScriptableObject.CreateInstance<SpellsCostBlood>(),
            ScriptableObject.CreateInstance<DamageDown>(),
            //ScriptableObject.CreateInstance<BulletSizeDown>(),
            ScriptableObject.CreateInstance<LifeStealDown>(),
            ScriptableObject.CreateInstance<ManaDown>(),
            ScriptableObject.CreateInstance<MaxHealthDown>(),
            ScriptableObject.CreateInstance<MovementSpeedDown>(),
            ScriptableObject.CreateInstance<RangeDown>()
        };
        this.debuffLength = this.debuffs.Count;
    }

}