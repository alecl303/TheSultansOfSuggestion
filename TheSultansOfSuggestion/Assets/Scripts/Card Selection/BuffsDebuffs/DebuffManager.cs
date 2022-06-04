using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Effect;
public class DebuffManager : MonoBehaviour
{
    List<IPlayerEffect> debuffs;
    [SerializeField] string description;

    private void Awake()
    {
        InitDebuffs();
    }

    public IPlayerEffect GetRandomDebuff()
    {
        return(this.debuffs[Random.Range(0, this.debuffs.Count)]);
    }

    void InitDebuffs(){
        this.debuffs = new List<IPlayerEffect>() {
            ScriptableObject.CreateInstance<BulletSpeedDown>(),
            ScriptableObject.CreateInstance<CritChanceDown>(),
            ScriptableObject.CreateInstance<DamageDown>(),
            ScriptableObject.CreateInstance<BackwardsBulletEffect>(),
            //ScriptableObject.CreateInstance<DropTo1HP>(),
            ScriptableObject.CreateInstance<InvertControls>(),
            ScriptableObject.CreateInstance<LoseActiveSpell>(),
            ScriptableObject.CreateInstance<SpellsCostBlood>(),
            ScriptableObject.CreateInstance<DamageDown>(),
            //ScriptableObject.CreateInstance<BulletSizeDown>(),
            ScriptableObject.CreateInstance<LifeStealDown>(),
            ScriptableObject.CreateInstance<ManaDown>(),
            ScriptableObject.CreateInstance<MaxHealthDown>(),
            ScriptableObject.CreateInstance<MovementSpeedDown>(),
            ScriptableObject.CreateInstance<RangeDown>()
        };
    }

    public List<IPlayerEffect> GetList()
    {
        return debuffs;
    }
}
