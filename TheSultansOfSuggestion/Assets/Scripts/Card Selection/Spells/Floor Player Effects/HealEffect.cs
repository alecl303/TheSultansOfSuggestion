using UnityEngine;
using System.Collections;

public class HealEffect : MonoBehaviour, IPlayerFloorSpellEffect
{
    [SerializeField] private float flatHeal;
    private bool overlap = false;
    private float timeBeforeHeal = 1.0f;
    
    public void SetFlatHeal(float flatHeal)
    {
        this.flatHeal = flatHeal;
    }
    public void SetOverlap(bool overlap)
    {
        this.overlap = overlap;
    }
    public void SetTimeBeforeHeal(float timeBeforeHeal){
        this.timeBeforeHeal = timeBeforeHeal;
    }
    void OnDestroy()  
    {   
        overlap = false;
    }
    public IEnumerator ApplyEffect(PlayerController target)
    {
        var playerStats = target.GetStats();
        while(overlap) 
        {
            playerStats.Heal(flatHeal);
            yield return new WaitForSeconds(timeBeforeHeal);
        }
    }
}
