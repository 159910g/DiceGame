using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : BattleCharacter
{
    private EnemyBase enemyBase;
    [SerializeField] SpriteRenderer nextActionIndicator;
    private EnemyAction nextAction;

    public EnemyAction NextAction 
    {
        get {return nextAction;} 
    }

    void Awake() //this needs to be awake to run before TargetOffset's Start function
    {
        if (characterBase == null)
        {
            gameObject.SetActive(false);
        }

        else
        {
            enemyBase = (EnemyBase)characterBase;
            base.SetData();
            ChooseNextAction();
        }
    }

    public void ChooseNextAction()
    {
        int index = Random.Range(0, enemyBase.Actions.Count);
        nextAction = enemyBase.Actions[index];
        nextActionIndicator.sprite = enemyBase.ActionIndicators[index];
    }

    public override void ShowInfo()
    {
        Debug.Log("Inside enemy showinfo()");
        // If a card is selected, clicking on an enemy will play the card instead
        if (BattleSystem.Instance.CardSelected == null)
        {            
            InfoBox.Instance.SetInfo(enemyBase.CharacterName, currentHP, enemyBase.MaxHP, nextAction, base.statusAilments);
        
            base.ShowHPDetails();
        }
    }
}
