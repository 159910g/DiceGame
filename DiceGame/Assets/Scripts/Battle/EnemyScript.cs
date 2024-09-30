using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : BattleCharacter
{
    private EnemyBase enemyBase;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] SpriteRenderer nextActionIndicator;
    [SerializeField] Slider healthBar;
    private EnemyAction nextAction;

    void Start()
    {
        //This is needed because Unity doesn't allow field type to ba altered in the child class
        enemyBase = (EnemyBase)characterBase;
        SetData();
        ChooseNextAction();
    }

    public void SetData()
    {
        base.SetData();
        healthBar.maxValue = enemyBase.MaxHP;
        healthBar.value = currentHP;
        spriteRenderer.sprite = enemyBase.EnemySprite;
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
            InfoBox.Instance.SetInfo(enemyBase.CharacterName, currentHP, enemyBase.MaxHP, nextAction, statusAilments);
        }
    }
}
