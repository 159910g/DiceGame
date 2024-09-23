using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public BattleCharacter player;
    public List<BattleCharacter> enemies;

    [SerializeField] GameObject cardTemplate;
    
    List<Card> ActiveDeck;
    List<SpawnableCard> spawnedDeck;
    List<SpawnableCard> cardsInHand;

    public void Start()
    {
        cardsInHand = new List<SpawnableCard>();
        spawnedDeck = new List<SpawnableCard>();
        SetupBattle();
    }

    public void SetupBattle()
    {
        ActiveDeck = Deck.Instance.ShuffleDeck();
        foreach(Card c in ActiveDeck)
        {
            SpawnableCard card = Instantiate(cardTemplate, new Vector3(0,0,0), Quaternion.identity).GetComponent<SpawnableCard>();

            card.Frame.sprite = c.Frame;
            card.Name.text = c.Name;
            card.gameObject.SetActive(false);

            spawnedDeck.Add(card);

            Debug.Log(c.Name);
        }

        DrawOpeningHand();
    }

    //draw opening hand (3)
    public void DrawOpeningHand()
    {
        Draw();
        Draw();
        Draw();
    }

    //eventually this will take in a postion
    public void Draw()
    {
        spawnedDeck[0].gameObject.SetActive(true);
        cardsInHand.Add(spawnedDeck[0]);
        spawnedDeck.RemoveAt(0);
        SpawnableCardsLocations.Instance.ReorientCardsInHand(cardsInHand);
    }
    //check item for mulligan


    //**START OF PLAYER TURN**

    //player rolls dice
    //Check for status ailment

    //player picks card and/or target plays card
    //player picks consumable items

    //handle card keywords before damage

    //call dmg calc

    //Check for status ailment
    //**END TURN OF PLAYER TURN**




    //**START OF ENEMY TURN**
    
    //Check for status ailment

    //roll dmg value and/or armour value

    //Check for status ailment
    //**END TURN OF ENEMY TURN**


    //**HANDLE DAMAGE**

    //hp = hp + armour - atk

}
