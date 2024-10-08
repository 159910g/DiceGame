using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    [SerializeField] List<CardBase> rawCards;

    [SerializeField] GameObject cardTemplate;

    public List<Card> PlayerDeck = new List<Card>();

    public List<Card> ShuffledDeck = new List<Card>();

    public List<SpawnableCard> Graveyard = new List<SpawnableCard>(); 

    public List<SpawnableCard> spawnedDeck = new List<SpawnableCard>();

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
            card.cardBase = rawCard;
            card.InitValues();
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

    public void StartBattle()
    {
        ShuffledDeck = ShuffleDeck();
        foreach(Card c in ShuffledDeck)
        {
            SpawnableCard card = Instantiate(cardTemplate, new Vector3(0,0,0), Quaternion.identity).GetComponent<SpawnableCard>();

            card.Init(c);

            card.gameObject.SetActive(false);

            spawnedDeck.Add(card);

            //Debug.Log(c.Name);
        }
    }

    public SpawnableCard Draw(int index=0)
    {
        Debug.Log("spawnedDeck.Count = "+ spawnedDeck.Count);
        if(spawnedDeck.Count == 0)
            ResetDeck();
        spawnedDeck[index].gameObject.SetActive(true);
        SpawnableCard cardDrawn = spawnedDeck[index];
        spawnedDeck.RemoveAt(index);
        return cardDrawn;
    }

    public void ToGraveyard(SpawnableCard card)
    {
        Graveyard.Add(card);
    }

    public void ResetDeck()
    {
        Debug.Log("Reset Deck");

        //for(int i = (Graveyard.Count - 1); i >= 0; i--)
        //{
        //    Debug.Log("here");
        //    Destroy(Graveyard[i]);
        //}

        spawnedDeck.Clear();
        //Graveyard.Clear();
        StartBattle();
    }

}
