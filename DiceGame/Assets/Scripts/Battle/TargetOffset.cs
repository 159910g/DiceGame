using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetOffset : MonoBehaviour
{
    [SerializeField] int offsetValueX;
    [SerializeField] int offsetValueY;

    bool isHovered = false;
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
        if (BattleSystem.Instance.CardSelected != null)
        {
            if(BattleSystem.Instance.CheckEnergyCost())
            {
                BattleSystem.Instance.PlayCard();
            }
        }
    }

    public void ResolveCard(Card card)
    {
        Debug.Log(gameObject.name +" Affected by "+ card.Name);
    }
}
