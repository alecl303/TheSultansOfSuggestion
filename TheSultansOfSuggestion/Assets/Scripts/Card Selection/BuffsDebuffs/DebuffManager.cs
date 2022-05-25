using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Effect;
public class DebuffManager : MonoBehaviour
{
    List<IPlayerEffect> debuffs;
    IPlayerEffect randomDebuff;
    [SerializeField] string description;
    private int debuffLength;
    // Start is called before the first frame update
    void Start()
    {
        this.debuffs = new List<IPlayerEffect>() {
            ScriptableObject.CreateInstance<BulletSpeedDown>(),
            ScriptableObject.CreateInstance<CritChanceDown>(),
            ScriptableObject.CreateInstance<DamageDown>(),
        };

        this.debuffLength = this.debuffs.Count;

        this.randomDebuff = this.debuffs[Random.Range(0, this.debuffLength)];

        this.description = this.randomDebuff.GetDescription();
    }

    public IPlayerEffect GetRandomDebuff()
    {
        return(this.debuffs[Random.Range(0, this.debuffLength)]);
    }

}
