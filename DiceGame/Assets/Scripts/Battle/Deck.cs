using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] List<CardBase> rawCards;

    public List<Card> PlayerDeck = new List<Card>();

    public static Deck Instance { get; private set; }

    public void Awake()
    {
        Instance = this;
        SetData();
    }

    public void SetData()
    {
        foreach(CardBase rawCard in rawCards)
        {
            Card card = new Card();

            card.Name = rawCard.name;

            card.ATKEnergyCost = rawCard.ATKEnergyCost;
            card.DEFEnergyCost = rawCard.DEFEnergyCost;
            card.UTLEnergyCost = rawCard.UTLEnergyCost;

            card.ATKValue = rawCard.ATKValue;
            card.DEFValue = rawCard.DEFValue;

            card.Targets = new List<bool>(rawCard.Targets); // Ensure lists are properly initialized
            
            card.Keywords = new List<string>(rawCard.Keywords);
            card.KeywordPotency = new List<int>(rawCard.KeywordPotency);

            card.GridImage = rawCard.GridImage;
            card.Frame = rawCard.Frame;

            PlayerDeck.Add(card);
        }
    }    

    public List<Card> ShuffleDeck()
    {
        List<Card> ShuffledDeck = new List<Card>(PlayerDeck);

        for (int i = 0; i < ShuffledDeck.Count; i++)
        {
            int randomIndex = Random.Range(i, ShuffledDeck.Count); // Get random index from current index to the end of the list
            // Swap cards at index i and randomIndex
            Card temp = ShuffledDeck[i];
            ShuffledDeck[i] = ShuffledDeck[randomIndex];
            ShuffledDeck[randomIndex] = temp;
        }

        return ShuffledDeck;
    }
}
