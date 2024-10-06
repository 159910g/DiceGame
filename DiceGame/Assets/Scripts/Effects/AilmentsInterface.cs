using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//called in game, cards hold a reference to them
public interface AilmentsInterface
{
    string AilmentName { get; }  // Property for the keyword's name

    string AilmentDescription { get; }  // Property for the keyword's description

    Sprite AilmentIcon { get; }

    string TriggerCondition { get; }
    
    //potenecy value = 0 when keyword does not require potency
    virtual void AilmentEffect(int potenecy, BattleCharacter target)
    {
        return;
    }
}

[System.Serializable]
public enum AilmentTriggerCondition
{
    Potency,
    StartOfTurn,
    EndOfTurn,
    OnAttck, //would be every time player plays card or enemy has an action
    OnRecieveDamage,
    OnRecieveHP
}
