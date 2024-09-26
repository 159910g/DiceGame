using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public static BattleSystem Instance;
    public BattleCharacter player;
    public List<BattleCharacter> enemies;
    
    List<Card> ActiveDeck;
    List<SpawnableCard> spawnedDeck;
    List<SpawnableCard> cardsInHand;

    public List<DieScript> dice;
    [SerializeField] Energy energy;

    public void Start()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        cardsInHand = new List<SpawnableCard>();
        SetupBattle();
    }

    public void SetupBattle()
    {
        Deck.Instance.StartBattle();
        DrawOpeningHand();
    }

    //draw opening hand (3)
    public void DrawOpeningHand()
    {
        Draw();
        Draw();
    }

    public void Draw(int deckPos=0)
    {
        cardsInHand.Add(Deck.Instance.Draw(deckPos));
        SpawnableCardsLocations.Instance.ReorientCardsInHand(cardsInHand);
    }

    public void SelectCard(SpawnableCard card)
    {
        foreach (SpawnableCard c in cardsInHand)
        {
            if (c != card)
            {
                c.SetIsSelected(false);
                c.OnMouseExit();
            }
            else 
            {
                c.SetIsSelected(true);
                TargetHandler.Instance.SetTargets(c.card.Targets);
            }
        }
    }

    public void RollDice()
    {
        foreach (DieScript die in dice)
        {
            DieResult result = die.RollDie();
            energy.ChangeEnergy(result.ATKEnergy, result.DEFEnergy, result.UTLEnergy);

            //Handle Effects
        }
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
