using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardShop : ShopBase, ShopInterface
{  
    [SerializeField] Button enterButton;
    [SerializeField] Button leaveButton;

    [SerializeField] Button backButton;
    [SerializeField] List<BuyableCard> buyableCards;
    
    public static CardShop Instance;

    private void Awake() {
        Instance = this;
    }

    public void SelectCard(SpawnableCard card)
    {
        foreach (BuyableCard bc in buyableCards)
        {
            SpawnableCard c = bc.GetCard();

            if (c != card)
            {
                c.SetIsSelected(false);
                c.OnMouseExit();
            }
            else 
            {
                c.SetIsSelected(true);
            }
        }
    }

    public void Enter()
    {
        enterButton.gameObject.SetActive(false);
        leaveButton.gameObject.SetActive(false);

        backButton.gameObject.SetActive(true);

        Debug.Log("Enter");
        for(int i = 0; i < buyableCards.Count; i++)
        {
            Card card = new Card();
            card.cardBase = Merchant.Cards[i];
            card.InitValues();

            buyableCards[i].SetCard(card);
            buyableCards[i].gameObject.SetActive(true);
        }
    }

    public void Back()
    {
        enterButton.gameObject.SetActive(true);
        leaveButton.gameObject.SetActive(true);

        backButton.gameObject.SetActive(false);

        foreach(BuyableCard bc in buyableCards)
        {
            bc.gameObject.SetActive(false);
        }
    }
}
