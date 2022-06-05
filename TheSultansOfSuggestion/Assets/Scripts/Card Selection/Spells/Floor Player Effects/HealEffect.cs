using UnityEngine;
using System.Collections;

public class HealEffect : MonoBehaviour, IPlayerFloorSpellEffect
{
    [SerializeField] private float flatHeal;
    private bool overlap = false;
    private float timeBeforeHeal = 2.0f;

    // Keep track if if the effect is already active 
    Coroutine activated = null;
    
    public void SetFlatHeal(float flatHeal)
    {
        this.flatHeal = flatHeal;
    }
    public void SetOverlap(bool overlap)
    {
        this.overlap = overlap;
        if (activated != null && overlap == false) 
        {
            StopCoroutine(activated);
            activated = null;
        }
    }
    public void SetTimeBeforeHeal(float timeBeforeHeal)
    {
        this.timeBeforeHeal = timeBeforeHeal;
    }

    void OnDestroy()  
    {   
        // Stop the effect and set overlap to false once the floor spell is destroyed.
        SetOverlap(false);
    }

    public void ApplyEffect(PlayerController target)
    {
        if (activated == null)
        {
            activated = StartCoroutine(Effect(target));
        }
    }

    public IEnumerator Effect(PlayerController target)
    {
        Debug.Log("Start!");
        var playerStats = target.GetStats();
        while(overlap) 
        {
            yield return new WaitForSeconds(timeBeforeHeal);
            Debug.Log("Heal!"); 
            playerStats.Heal(flatHeal);
            var popup = DamageNumber.CreatePopup(this.gameObject.GetComponent<Rigidbody2D>().position, flatHeal, false, false, true);
        }
    }
    
}
