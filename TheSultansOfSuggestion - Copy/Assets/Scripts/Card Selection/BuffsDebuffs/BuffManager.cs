using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Effect;
public class BuffManager : MonoBehaviour
{
    List<IPlayerEffect> buffs;
    IPlayerEffect randomBuff;

    [SerializeField] string description;
    private int buffLength = 0;
    

    public IPlayerEffect GetRandomBuff()
    {
        if(this.buffLength == 0)
        {
            initBuffs();
        }

        return(this.buffs[Random.Range(0, this.buffLength)]);
    }

    void initBuffs(){
        this.buffs = new List<IPlayerEffect>() {
            ScriptableObject.CreateInstance<BulletSpeedUp>(),
            ScriptableObject.CreateInstance<CritChanceUp>(),
            ScriptableObject.CreateInstance<DamageUp>(),
            ScriptableObject.CreateInstance<DoubleShot>(),
            ScriptableObject.CreateInstance<TripleShot>(),
            //ScriptableObject.CreateInstance<BulletSizeUp>(),
            ScriptableObject.CreateInstance<CritMultiplierUp>(),
            ScriptableObject.CreateInstance<Heal20>(),
            ScriptableObject.CreateInstance<LifeStealUp>(),
            ScriptableObject.CreateInstance<ManaUp>(),
            ScriptableObject.CreateInstance<MaxHealthUp>(),
            ScriptableObject.CreateInstance<MovementSpeedUp>(),
            ScriptableObject.CreateInstance<PoisonChanceUp>(),
            ScriptableObject.CreateInstance<PoisonDamageUp>(),
            ScriptableObject.CreateInstance<PoisonTimeUp>(),
            ScriptableObject.CreateInstance<RangeUp>(),
            ScriptableObject.CreateInstance<StunChanceUp>(),
        };
        this.buffLength = this.buffs.Count;
    }
}
