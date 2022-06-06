using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{
    private const float TIME_TO_DISAPPEAR = 1.0f;
    private static int sortingOrder;

    private TextMeshPro text;
    private Color textColor;
    private Vector3 displacementVector;

    private float disappearTimer;
    private int fontSize;

    void Awake()
    {
        this.text = transform.GetComponent<TextMeshPro>();
    }

    void Update(){
        transform.position += this.displacementVector * Time.deltaTime;
        this.displacementVector -= this.displacementVector * 6f * Time.deltaTime;

        if(this.disappearTimer > TIME_TO_DISAPPEAR * 0.15f)
        {
            transform.localScale += Vector3.one * Time.deltaTime * 0.2f;
        }
        else
        {
            float vanishingSpeed = 15.0f;
            textColor.a -= vanishingSpeed * Time.deltaTime;
            text.faceColor = textColor;
            if(textColor.a < 0)
            {
                // Once the popup has completely vanished, destroy it
                Destroy(gameObject);
            }
        }
        this.disappearTimer -= Time.deltaTime;
    }

    public static DamageNumber CreatePopup(Vector3 enemyPosition, float damageAmount, bool isCrit, bool isPoison)
    {
        Transform damageNumberTransform = Instantiate(DamageAsset.instance, enemyPosition, Quaternion.identity);

        DamageNumber popup = damageNumberTransform.GetComponent<DamageNumber>();
        popup.InitPopup(isCrit, isPoison);
        popup.SetPopupDamage(damageAmount);

        return popup;
    }

    public void SetPopupDamage(float damageAmount)
    {
        // Set text number as int to avoid decimals
        text.SetText(((int)damageAmount).ToString());
    }

    public void InitPopup(bool isCrit, bool isPoison)
    {
        if(isCrit)
        {
            this.fontSize = 24;
            this.textColor = new Color32(231, 40, 40, 255);
        }
        else if (isPoison)
        {
            this.fontSize = 16;
            this.textColor = new Color32(13, 170, 21, 255);
        }
        else
        {
            this.fontSize = 16;
            this.textColor = new Color32(255, 255, 255, 255);
        }

        
        text.fontSize = this.fontSize;
        text.faceColor = this.textColor;
        this.disappearTimer = TIME_TO_DISAPPEAR;
        this.displacementVector = new Vector3(0.5f, 1f) * 10.0f;

        sortingOrder++;
        text.sortingOrder = sortingOrder;
    }
}
