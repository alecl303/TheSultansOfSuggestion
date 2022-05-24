using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] List<Sprite> sprites;

    private int damage;
    private Sprite sprite;
    private string rarity;
    private int lowerBound;
    private int upperBound;
    // Start is called before the first frame update
    void Start()
    {
        SetRarity();
        this.damage = Random.Range(this.lowerBound, this.upperBound);
        Debug.Log(this.damage);
        Debug.Log(this.rarity);
        var spriteIndex = Random.Range(0, 30);
        this.sprite = this.sprites[spriteIndex];

        this.gameObject.GetComponent<SpriteRenderer>().sprite = this.sprite;
    }

    public int GetDamage()
    {
        return this.damage;
    }

    private void SetRarity()
    {
        int randomValue = Random.Range(0, 100);

        if(randomValue < 5)
        {
            this.rarity = "Legendary";
            this.lowerBound = 8;
            this.upperBound = 10;
        }
        else if (randomValue < 20)
        {
            this.rarity = "Rare";
            this.lowerBound = 6;
            this.upperBound = 9;
        }
        else if (randomValue < 50)
        {
            this.rarity = "Good";
            this.lowerBound = 4;
            this.upperBound = 7;
        }
        else
        {
            this.rarity = "Common";
            this.lowerBound = 3;
            this.upperBound = 5;
        }
    }

    private string GetRarity()
    {
        return this.rarity;
    }
}
