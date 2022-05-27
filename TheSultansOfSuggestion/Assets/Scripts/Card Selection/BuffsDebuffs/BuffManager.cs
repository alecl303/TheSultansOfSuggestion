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
    // Start is called before the first frame update
    // void Start()
    // {
    //     this.buffs = new List<IPlayerEffect>() {
    //         ScriptableObject.CreateInstance<BulletSpeedUp>(),
    //         ScriptableObject.CreateInstance<CritChanceUp>(),
    //         ScriptableObject.CreateInstance<DamageUp>(),
    //     };
    //     this.buffLength = this.buffs.Count;


    // }

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
        };
        this.buffLength = this.buffs.Count;
    }
}
