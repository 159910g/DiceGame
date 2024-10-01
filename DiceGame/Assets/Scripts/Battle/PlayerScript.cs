using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : BattleCharacter
{

    void Awake() //this needs to be awake to run before TargetOffset's Start function
    {
        base.SetData();
    }

    public void ChooseNextAction()
    {
        return;
    }

    public override void ShowInfo()
    {
        //Debug.Log("Inside enemy showinfo()");
        // If a card is selected, clicking on an enemy will play the card instead
        //if (BattleSystem.Instance.CardSelected == null)
        //{
        //    InfoBox.Instance.SetInfo(enemyBase.CharacterName, currentHP, enemyBase.MaxHP, nextAction, base.statusAilments);
        //}
    }
}
