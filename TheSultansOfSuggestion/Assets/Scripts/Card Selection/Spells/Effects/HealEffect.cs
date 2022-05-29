using UnityEngine;
using System.Collections;

public class HealEffect : MonoBehaviour, ISpellEffect
{
    // [SerializeField] private GameObject target;
    [SerializeField] private float flatHeal;
    private bool overlap = false;
    // private PlayerStats target;

    public void SetFlatHeal(float flatHeal)
    {
        this.flatHeal = flatHeal;
    }
    public void SetOverlap(bool overlap)
    {
        this.overlap = overlap;
    }
    public IEnumerator ApplyEffect(PlayerController target)
    {
        Debug.Log("Applying effect");
        // Debug.Log(target);
        var playerStats = target.GetStats();
        while(overlap) 
        {
        // this.target.GetComponent<PlayerController>().GetStats().Heal(flatHeal*statsMultiplier);
            Debug.Log("Healing");
            playerStats.Heal(flatHeal*playerStats.SpellStrength);
            yield return new WaitForSeconds(1.0f);
        }
        Debug.Log("Finished applying effect");
    }
}
