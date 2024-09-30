using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleCharacter : MonoBehaviour
{
    public int currentHP;
    public CharacterBase characterBase;
    public Dictionary<AilmentsInterface, int> statusAilments = new Dictionary<AilmentsInterface, int>(); 
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] protected Slider healthBar;
    public List<AilmentsInterface> ailmentsToCure = new List<AilmentsInterface>();

    public void SetData()
    {
        currentHP = characterBase.MaxHP;
        healthBar.maxValue = characterBase.MaxHP;
        healthBar.value = currentHP;
        spriteRenderer.sprite = characterBase.CharacterSprite;
    }

    public virtual void ShowInfo()
    {
        return;
    }

    public bool TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Debug.Log(characterBase.CharacterName + " Died!");
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
