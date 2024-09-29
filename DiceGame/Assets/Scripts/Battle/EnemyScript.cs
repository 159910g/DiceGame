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
        Debug.Log("Choosing Action");
        int index = Random.Range(0, enemyBase.Actions.Count);
        EnemyAction action = enemyBase.Actions[index];
        nextActionIndicator.sprite = enemyBase.ActionIndicators[index];
        Debug.Log("Next Action is " + action.Description);
    }
}
