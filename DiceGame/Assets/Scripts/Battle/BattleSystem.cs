using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState
{
    PlayerTurn,
    EnemyTurn,
    Busy

}

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

    Card cardSelected;

    public BattleState state = BattleState.Busy;

    NotificationPopupController NPopup;

    public Card CardSelected
    {
        get { return cardSelected; }
    }

    public void ClearCardSelected()
    {
        cardSelected = null;

        foreach(SpawnableCard c in cardsInHand)
        {
            StartCoroutine(c.ReturnCard());
        }

        TargetHandler.Instance.TurnOffAllTargets();
    }

    public void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        NPopup = GetComponent<NotificationPopupController>();
        cardsInHand = new List<SpawnableCard>();
        SetupBattle();
    }

    public void SetupBattle()
    {
        //spawn in all cards in deck
        Deck.Instance.StartBattle();
        DrawOpeningHand();
        PlayerTurn();
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

    //spawnable card calls this
    //player picks card
    public void SelectCard(SpawnableCard card)
    {
        if(state == BattleState.PlayerTurn)
        {
            //go through every card in player hand and set their isSelected to false
            //unless the card in hand is the same card that called this method in which case
            //set it to true
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
                    cardSelected = c.card;
                }
            }
        }
    }

    public bool CheckEnergyCost()
    {
        bool atk = false;
        bool def = false;
        bool util = false;

        if(energy.atkEnergy >= cardSelected.ATKEnergyCost)
            atk = true;
        
        if(energy.defEnergy >= cardSelected.DEFEnergyCost)
            def = true;

        if(energy.utlEnergy >= cardSelected.UTLEnergyCost)
            util = true;

        if(atk && def && util)
        {   
            energy.ChangeEnergy(cardSelected.ATKEnergyCost*-1, cardSelected.DEFEnergyCost*-1, cardSelected.UTLEnergyCost*-1);
            return true;
        }

        else  
        {
            NPopup.NotEnoughMana();
            ClearCardSelected();
            return false;
        }
    }

    public void PlayCard()
    {
        for(int i = 0; i < cardsInHand.Count; i++)
        {
            if(cardsInHand[i].card == cardSelected)
            {
                TargetHandler.Instance.ResolveCard(cardSelected);
                cardsInHand[i].gameObject.SetActive(false);
                cardsInHand.RemoveAt(i);
                cardSelected = null;
                TargetHandler.Instance.TurnOffAllTargets();
                SpawnableCardsLocations.Instance.ReorientCardsInHand(cardsInHand);
            }
        }
    }

    public void RollDice()
    {
        foreach (DieScript die in dice)
        {
            die.RollDie((DieResult result) =>
            {
                energy.ChangeEnergy(result.ATKEnergy, result.DEFEnergy, result.UTLEnergy);
            });

            //Handle Effects
        }
    }

    //check item for mulligan

    //**START OF PLAYER TURN**
    public void PlayerTurn()
    {
        Draw();
        //player rolls dice
        RollDice();

        //Check for status ailment

        state = BattleState.PlayerTurn;

        //player picks consumable items

        //handle card keywords before damage

        //call dmg calc

        //Check for status ailment
        //**END TURN OF PLAYER TURN**
    }





    //**START OF ENEMY TURN**
    
    //Check for status ailment

    //roll dmg value and/or armour value

    //Check for status ailment
    //**END TURN OF ENEMY TURN**


    //**HANDLE DAMAGE**

    //hp = hp + armour - atk

}
