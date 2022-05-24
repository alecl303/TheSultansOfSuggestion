using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Effect;
public class BuffManager : MonoBehaviour
{
    List<IPlayerEffect> buffs;
    IPlayerEffect randomBuff;
    [SerializeField] string description;
    // Start is called before the first frame update
    void Start()
    {
        this.buffs = new List<IPlayerEffect>() {
            ScriptableObject.CreateInstance<BulletSpeedUp>(),
            ScriptableObject.CreateInstance<CritChanceUp>(),
            ScriptableObject.CreateInstance<DamageUp>(),
        };

        this.randomBuff = this.buffs[Random.Range(0, 3)];

        this.description = this.randomBuff.GetDescription();
    }

}
