using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Effect;
public class BuffManager : MonoBehaviour
{
    List<IPlayerEffect> buffs;
    [SerializeField] string description;

    private void Awake()
    {
        InitBuffs();
    }

    public IPlayerEffect GetRandomBuff()
    {
        return(this.buffs[Random.Range(0, this.buffs.Count)]);
    }

    void InitBuffs(){
        this.buffs = new List<IPlayerEffect>() {
            ScriptableObject.CreateInstance<BulletSpeedUp>(),
            ScriptableObject.CreateInstance<CritChanceUp>(),
            ScriptableObject.CreateInstance<DamageUp>(),
            ScriptableObject.CreateInstance<DoubleShot>(),
            ScriptableObject.CreateInstance<TripleShot>(),
            //ScriptableObject.CreateInstance<BulletSizeUp>(),
            ScriptableObject.CreateInstance<CritMultiplierUp>(),
            //ScriptableObject.CreateInstance<Heal20>(),
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
    }

    public List<IPlayerEffect> GetList()
    {
        return buffs;
    } 
}
