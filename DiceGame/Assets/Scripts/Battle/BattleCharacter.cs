using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class BattleCharacter : MonoBehaviour
{
    public int currentHP;

    int currentArmour;
    public CharacterBase characterBase;
    public Dictionary<AilmentsInterface, int> statusAilments = new Dictionary<AilmentsInterface, int>(); 
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] protected Slider healthBar;
    [SerializeField] protected Slider damageBar;
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
        damageBar.maxValue = characterBase.MaxHP;
        damageBar.value = 0;
        spriteRenderer.sprite = characterBase.CharacterSprite;
        currentArmour = 0;
        ChangeCurrentArmour(0);
        healthBar.GetComponentInChildren<TextMeshPro>().enabled = false;
    }

    public virtual void ShowInfo()
    {
        return;
    }

    public bool TakeDamage(int damage)
    {
        if(characterBase != null)
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
                characterBase = null;
                return true;
            }

            healthBar.value = currentHP;
        }
        return false;
    }

    public void TriggerAilments(string timing)
    {
        foreach (AilmentsInterface ailment in statusAilments.Keys)
        {
            if(timing == ailment.TriggerCondition)
            {
                Debug.Log("Triggering " + ailment);
                ailment.AilmentEffect(statusAilments[ailment], this);
            }
        }

        //foreach (AilmentsInterface toCure in ailmentsToCure)
        //{
        //    statusAilments.Remove(toCure);
        //}

        //ailmentsToCure.Clear();
    }

    public void ShowHPDetails()
    {
        InfoBox.Instance.OnHideInfo += HideInfo;

        //make health bar big during targetting
        if(BattleSystem.Instance.CardSelected != null && characterBase != null)
        {
            int damage = BattleSystem.Instance.CardSelected.ATKValue;

            if(currentArmour - damage >= 0)
            {
                armourText.text = (currentArmour - damage).ToString();
            }
            else
            {   
                //if current was > 0 and the current damage would not be enough to break it
                //then we wouldnt be in this else statement
                if(currentArmour > 0)
                    armourText.text = "0";

                int HPafterATK = currentHP + (currentArmour - damage); //currentArmour - damage is a negative number;
                
                if(HPafterATK < 0)
                    HPafterATK = 0;

                healthBar.GetComponentInChildren<TextMeshPro>().text = HPafterATK +"/"+ characterBase.MaxHP;
                GetComponent<HPAnimation>().FadeHP(HPafterATK);
            }
        }

        //make healthbar big during inspection
        else
            healthBar.GetComponentInChildren<TextMeshPro>().text = currentHP +"/"+ characterBase.MaxHP;
        
        healthBar.GetComponentInChildren<TextMeshPro>().enabled = true;
        healthBar.transform.DOScaleY(2, 0.25f);
    }

    public void HideInfo()
    {
        //stop coroutine
        GetComponent<HPAnimation>().StopDmgFade();
        //set damagebar to 0 (empty)
        damageBar.value = 0;
        //set armour back to normal
        armourText.text = currentArmour.ToString();
        //set hp back to normal
        healthBar.value = currentHP;
        //turn off text
        healthBar.GetComponentInChildren<TextMeshPro>().enabled = false;
        //back to normal
        healthBar.transform.DOScaleY(1, 0.25f);
    }
}
