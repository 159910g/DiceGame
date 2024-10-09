using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBase : MonoBehaviour, ShopInterface
{
    [SerializeField] MerchantBase merchant;

    public string ShopName { get => merchant.Name; }

    public string GreetingDialog { get => merchant.Greetings; }

    public MerchantBase Merchant { get => merchant; }
    // Start is called before the first frame update
    public virtual void BuyItem()
    {
        return;
    }

    public virtual void DiscardItem()
    {
        return;
    }

    public virtual void Leave()
    {
        //return to map
        return;
    }

    public virtual void Enter()
    {
        //show items
        return;
    }
}
