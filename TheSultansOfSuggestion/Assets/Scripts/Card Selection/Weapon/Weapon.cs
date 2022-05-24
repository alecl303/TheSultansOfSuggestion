using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public List<string> sprites;

    private int damage;
    public Sprite sprite;
    public int spriteIndex;
    private string rarity;
    private int lowerBound;
    private int upperBound;
    // Start is called before the first frame update
    void Awake()
    {
        this.sprites = new List<string> {
            "File_0", "File_1","File_2","File_3","File_4","File_5","File_6","File_7","File_8","File_9",
            "File_10","File_11","File_12","File_13","File_14","File_15","File_16","File_17","File_18","File_19",
            "File_20","File_21","File_22","File_23","File_24","File_25","File_26","File_27","File_28","File_29"
        };
        SetRarity();
        this.damage = Random.Range(this.lowerBound, this.upperBound);
        this.spriteIndex = Random.Range(0, 30);
        this.sprite = Resources.Load<Sprite>(this.sprites[this.spriteIndex]);

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

    public Sprite GetSprite()
    {
        return this.sprite;
    }
}
