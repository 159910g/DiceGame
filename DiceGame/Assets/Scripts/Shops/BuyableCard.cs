using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyableCard : MonoBehaviour
{
    [SerializeField] SpawnableCard card;
    [SerializeField] Button buyButton;

    CardShop shop;

    private void Start() {
        gameObject.SetActive(false);
    }

    public void SetCard(Card input)
    {
        card.Init(input);
    }

    public SpawnableCard GetCard()
    {
        return card;
    }

    public void SetCardShop(CardShop shop)
    {
        shop = this.shop;
    }

    public void BuyCard()
    {
        Deck.Instance.PlayerDeck.Add(card.card);

        Debug.Log("----DECK----");
        foreach(Card c in Deck.Instance.PlayerDeck)
        {
            Debug.Log(c.Name);
        }
    }
}
