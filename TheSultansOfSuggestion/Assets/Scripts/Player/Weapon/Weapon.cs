using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    private int damage;
    public Sprite sprite;

    // Start is called before the first frame update
    void Start()
    {
        var weaponSpecs = this.GetComponent<WeaponSpecs>();

        this.damage = weaponSpecs.damage;
        this.sprite = weaponSpecs.sprite;

        this.gameObject.GetComponent<SpriteRenderer>().sprite = this.sprite;
    }

    public int GetDamage()
    {
        return this.damage;
    }
}
