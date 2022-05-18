using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpecs: MonoBehaviour
{
    public int damage;
    public int attackSpeed;
    public Sprite sprite;

    [SerializeField] List<Sprite> sprites;


    void Start()
    {
        // Rarity will determine ranges
        this.damage = Random.Range(4, 7);

        var spriteIndex = Random.Range(0, 30);

        this.sprite = this.sprites[spriteIndex];
    }
}
