using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetOffset : MonoBehaviour
{
    [SerializeField] int offsetValueX;
    [SerializeField] int offsetValueY;

    bool isHovered = false;

    public BattleCharacter character = null;
    [SerializeField] BoxCollider2D collider2D;

    void Start()
    {
        character = GetCharacter();
        if (collider2D == null) collider2D = GetComponent<BoxCollider2D>();
        BattleSystem.Instance.cardSelectEvent.AddListener(EnablePlay);
        BattleSystem.Instance.cardDeselectEvent.AddListener(DisablePlay);
    }

    public void EnablePlay() //All squares can be targets for cards
    {
        collider2D.enabled = true;
    }

    public void DisablePlay() //Only squares with characters can be targeted for info
    {
        if (character == null)
        {
            collider2D.enabled = false;
        }
    }

    public void OnMouseOver()
    {
        if (!isHovered && BattleSystem.Instance.CardSelected != null)
        {
            Debug.Log(gameObject.name);
            TargetHandler.Instance.TargetOffset(offsetValueX,  offsetValueY);
            isHovered = true;
        }
    }

    public void OnMouseExit()
    {
        if (isHovered)
        {
            isHovered = false;
        }
    }

    public void OnMouseDown()
    {
        if(this.enabled)
        {
            if (BattleSystem.Instance.CardSelected != null)
            {
                if(BattleSystem.Instance.CheckEnergyCost())
                {
                    BattleSystem.Instance.PlayCard();
                }
            }

            else if (character != null)
            {
                character.ShowInfo();
            }
        }
    }

    public void CurrentlyTargetted(bool value)
    {
        if (character != null)
        {
            if(value)   
                character.ShowHPDetails();

            else
                character.HideInfo();   
        }     
    } 

    public void ResolveCard(Card card)
    {
        Debug.Log(gameObject.name +" Affected by "+ card.Name);
        if (character != null)
        {
            if(card.Keywords.Count > 0)
            {
                foreach(string k in card.Keywords)
                {
                    if(AllKeywords.Instance.KeywordAffectsTarget(k))
                        AllKeywords.Instance.UseKeywordEffect(k, card.GetKeywordValue(k), character);
                }
            }

            if(character.TakeDamage(card.ATKValue)) //dead
            {
                character = null;
            }
        }
    }

    public BattleCharacter GetCharacter()
    {
        return GetComponentInChildren<BattleCharacter>();
    }
}
