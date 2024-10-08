using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//called in game, cards hold a reference to them
public interface ShopInterface
{
    string ShopName { get; }  

    string GreetingDialog { get; } 
    
    virtual void BuyItem()
    {
        return;
    }

    virtual void DiscardItem()
    {
        return;
    }

    virtual void Leave()
    {
        return;
    }
}