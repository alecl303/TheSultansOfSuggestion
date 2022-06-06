using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Sprite[] sprites;
    public int damage;
    public Sprite sprite;
    public int spriteIndex;
    private string rarity;
    private int lowerBound;
    private int upperBound;

    public void Randomize()
    {
        SetRarity();
        this.damage = Random.Range(this.lowerBound, this.upperBound);
        this.spriteIndex = Random.Range(0, 30);
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
            this.lowerBound = 10;
            this.upperBound = 12;
        }
        else if (randomValue < 20)
        {
            this.rarity = "Rare";
            this.lowerBound = 8;
            this.upperBound = 10;
        }
        else if (randomValue < 50)
        {
            this.rarity = "Good";
            this.lowerBound = 6;
            this.upperBound = 8;
        }
        else
        {
            this.rarity = "Common";
            this.lowerBound = 4;
            this.upperBound = 6;
        }
    }

    private string GetRarity()
    {
        return this.rarity;
    }

    public Sprite GetSprite()
    {
        return this.sprite;
    }

    public void SetSprite(Sprite sprite)
    {
        this.sprite = sprite;
    }

    public string GetDescription()
    {
        return (this.rarity + " weapon with " + this.damage + " damage.");
    }
}
