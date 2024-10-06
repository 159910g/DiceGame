using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BleedAilment : MonoBehaviour, AilmentsInterface
{
    [SerializeField] string ailmentName = "Bleed";
    [SerializeField] string ailmentDescription;

    [SerializeField] Sprite ailmentIcon;

    [SerializeField] AilmentTriggerCondition triggerCondition;

    public string AilmentName { get => ailmentName; }  // Property for the keyword's name

    public string TriggerCondition { get => triggerCondition.ToString(); }

    public string AilmentDescription { get => ailmentDescription; }  // Property for the keyword's description
    
    public Sprite AilmentIcon { get => ailmentIcon; }
    public void AilmentEffect(int potency, BattleCharacter target)
    {
        Debug.Log("Called BleedAilment.AilmentEffect " + potency);
        if (target.statusAilments[this] >= 2)
        {
            target.TakeDamage(4);
            target.ailmentsToCure.Add(this);
        }
    }
}
