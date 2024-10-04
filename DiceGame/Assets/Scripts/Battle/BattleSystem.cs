using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    [SerializeField] DMGAnimator dmgA;

    Card cardSelected;

    public BattleState state = BattleState.Busy;

    NotificationPopupController NPopup;

    public UnityEvent cardSelectEvent;
    public UnityEvent cardDeselectEvent;

    public void PlayDMGTextAnimation(Vector3 pos, int dmg)
    {
        DMGAnimator a = Instantiate(dmgA, pos, Quaternion.identity);
        a.SetData(dmg);
    }

    public Card CardSelected
    {
        get { return cardSelected; }
    }

    public void ClearCardSelected()
    {
        cardDeselectEvent.Invoke();
        cardSelected = null;

        foreach(SpawnableCard c in cardsInHand)
        {
            if(c.IsSelected)
                StartCoroutine(c.ReturnCard());

            c.isHovered = false;
        }

        TargetHandler.Instance.TurnOffAllTargets();
    }

    public void Awake()
    {
        if (Instance == null) Instance = this;
        if (cardSelectEvent == null) cardSelectEvent = new UnityEvent();
        if (cardDeselectEvent == null) cardDeselectEvent = new UnityEvent();
        NPopup = GetComponent<NotificationPopupController>();
        cardsInHand = new List<SpawnableCard>();
    }

    public void Start()
    {
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
        if(cardsInHand.Count < 10 && Deck.Instance.spawnedDeck.Count > 0)
        {
            cardsInHand.Add(Deck.Instance.Draw(deckPos));
            SpawnableCardsLocations.Instance.ReorientCardsInHand(cardsInHand);
        }
    }

    //spawnable card calls this
    //player picks card
    public void SelectCard(SpawnableCard card)
    {
        if(state == BattleState.PlayerTurn)
        {
            cardSelectEvent.Invoke();
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
            //need this if condition to know which spawnable card to manipulate
            if(cardsInHand[i].card == cardSelected)
            {
                if(cardSelected.Keywords.Count > 0)
                {
                    foreach(string k in cardSelected.Keywords)
                    {
                        if(!AllKeywords.Instance.KeywordAffectsTarget(k))
                            AllKeywords.Instance.UseKeywordEffect(k, cardSelected.GetKeywordValue(k));
                    }
                }

                player.GetComponent<Animations>().PlayAttackAnimation();

                //responible for determining which grid spaces are to be affected by the card (for damage/applying ailments)
                TargetHandler.Instance.ResolveCard(cardSelected);

                //removing card from hand, reorienting hand, misc
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
        state = BattleState.Busy;

        int remainingDice = dice.Count;
        foreach (DieScript die in dice)
        {
            die.RollDie((DieResult result) =>
            {
                //Handle Effects
                energy.ChangeEnergy(result.ATKEnergy, result.DEFEnergy, result.UTLEnergy);

                remainingDice--;

                // Once all dice have finished rolling, change state to PlayerTurn
                if (remainingDice == 0)
                {
                    state = BattleState.PlayerTurn;
                }
            });
        }
    }

    public void TriggerEnemyAilments()
    {
        Debug.Log("Calling TriggerEnemyAilments");
        foreach (BattleCharacter enemy in enemies)
        {
            enemy.TriggerAilments();
        }
    }

    public void StartEnemyTurn()
    {
        foreach(SpawnableCard c in cardsInHand)
        {
            c.DeselectCard();
        }
        StartCoroutine(EnemyTurn());
    }

    public IEnumerator EnemyTurn()
    {
        EnemyAction action = new EnemyAction();
        foreach (EnemyScript enemy in enemies)
        {
            if(enemy.characterBase != null)
            {
                action = enemy.NextAction;

                int dmg = Random.Range(action.MinATKValue, action.MaxATKValue);
                if(dmg > 0)
                {
                    enemy.GetComponent<Animations>().PlayAttackAnimation();
                    yield return new WaitForSeconds(0.2f);
                    player.TakeDamage(dmg);
                    Debug.Log(enemy.characterBase.CharacterName +" did "+ dmg);
                }

                int armour = Random.Range(action.MinDEFValue, action.MaxDEFValue);
                if(armour > 0)
                {
                    enemy.ChangeCurrentArmour(armour);
                }

                enemy.ChooseNextAction();   

                yield return new WaitForSeconds(0.5f);
            }
        }
        PlayerTurn();
    }
    //check item for mulligan

    //**START OF PLAYER TURN**
    public void PlayerTurn()
    {
        Draw();
        //player rolls dice
        Debug.Log(state);
        RollDice();

        //Check for status ailment
        //ailment checking will set state to busy
        Debug.Log(state);
        //state = BattleState.PlayerTurn;

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
