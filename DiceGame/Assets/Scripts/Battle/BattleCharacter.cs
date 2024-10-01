using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleCharacter : MonoBehaviour
{
    public int currentHP;

    int currentArmour;
    public CharacterBase characterBase;
    public Dictionary<AilmentsInterface, int> statusAilments = new Dictionary<AilmentsInterface, int>(); 
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] protected Slider healthBar;
    public List<AilmentsInterface> ailmentsToCure = new List<AilmentsInterface>();

    [SerializeField] TextMeshPro armourText;

    public SpriteRenderer SpriteRenderer
    {
        get {return spriteRenderer;}
    }

    public int CurrentArmour
    {
        get {return currentArmour;}
    }

    public void ChangeCurrentArmour(int value)
    {
        currentArmour += value;
        armourText.text = currentArmour.ToString();

        if(currentArmour <= 0)
        {
            armourText.gameObject.SetActive(false);
            currentArmour = 0;

        }
        else
            armourText.gameObject.SetActive(true);
        
    }

    public void SetData()
    {
        Debug.Log("SET DATA");
        currentHP = characterBase.MaxHP;
        healthBar.maxValue = characterBase.MaxHP;
        healthBar.value = currentHP;
        spriteRenderer.sprite = characterBase.CharacterSprite;
        currentArmour = 0;
        ChangeCurrentArmour(0);
    }

    public virtual void ShowInfo()
    {
        return;
    }

    public bool TakeDamage(int damage)
    {
        if(currentArmour - damage >= 0)
        {
            ChangeCurrentArmour(-damage);
        }
        else
        {
            currentHP += (currentArmour - damage); //currentArmour - damage is a negative number;
            ChangeCurrentArmour(-damage);
        }

        if(damage > 0)
        {
            BattleSystem.Instance.PlayDMGTextAnimation(transform.position, damage);
            GetComponent<Animations>().PlayHitAnimation();
        }

        if (currentHP <= 0)
        {
            Debug.Log(characterBase.CharacterName + " Died!");
            GetComponent<Animations>().PlayFaintAnimation();
            gameObject.SetActive(false);
            return true;
        }

        healthBar.value = currentHP;
        return false;
    }

    public void TriggerAilments()
    {
        foreach (AilmentsInterface ailment in statusAilments.Keys)
        {
            Debug.Log("Triggering " + ailment);
            ailment.AilmentEffect(statusAilments[ailment], this);
        }

        foreach (AilmentsInterface toCure in ailmentsToCure)
        {
            statusAilments.Remove(toCure);
        }

        ailmentsToCure.Clear();
    }
}
