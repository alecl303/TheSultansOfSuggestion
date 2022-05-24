using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player.Effect;
public class DebuffManager : MonoBehaviour
{
    List<IPlayerEffect> debuffs;
    IPlayerEffect randomDebuff;
    [SerializeField] string description;
    // Start is called before the first frame update
    void Start()
    {
        this.debuffs = new List<IPlayerEffect>() {
            ScriptableObject.CreateInstance<BulletSpeedDown>(),
            ScriptableObject.CreateInstance<CritChanceDown>(),
            ScriptableObject.CreateInstance<DamageDown>(),
        };

        this.randomDebuff = this.debuffs[Random.Range(0, 3)];

        this.description = this.randomDebuff.GetDescription();
    }

}
