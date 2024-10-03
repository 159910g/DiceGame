using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetOffset : MonoBehaviour
{
    [SerializeField] int offsetValueX;
    [SerializeField] int offsetValueY;

    bool isHovered = false;

    public BattleCharacter character = null;

    void Start()
    {
        GetCharacter();
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

    public void GetCharacter()
    {
        character = GetComponentInChildren<BattleCharacter>();
    }
}
