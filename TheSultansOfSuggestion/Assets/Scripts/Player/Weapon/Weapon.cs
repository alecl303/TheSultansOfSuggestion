using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] List<Sprite> sprites;

    private int damage;
    private Sprite sprite;

    // Start is called before the first frame update
    void Start()
    {
        this.damage = Random.Range(3, 7);

        var spriteIndex = Random.Range(0, 30);
        this.sprite = this.sprites[spriteIndex];

        this.gameObject.GetComponent<SpriteRenderer>().sprite = this.sprite;
    }

    public int GetDamage()
    {
        return this.damage;
    }
}
