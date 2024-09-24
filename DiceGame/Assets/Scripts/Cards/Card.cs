using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    public CardBase cardBase;
    public string Name;
    public int ATKEnergyCost;
    public int DEFEnergyCost;
    public int UTLEnergyCost;

    public int ATKValue;
    public int DEFValue;

    public List<bool> Targets;

    public List<string> Keywords;
    public List<int> KeywordPotency;

    public Sprite Frame;
    public Sprite GridImage;

    public void InitValues()
    {
        this.Name = cardBase.name;

        this.ATKEnergyCost = cardBase.ATKEnergyCost;
        this.DEFEnergyCost = cardBase.DEFEnergyCost;
        this.UTLEnergyCost = cardBase.UTLEnergyCost;

        this.ATKValue = cardBase.ATKValue;
        this.DEFValue = cardBase.DEFValue;

        this.Targets = new List<bool>(cardBase.Targets); // Ensure lists are properly initialized
        
        this.Keywords = new List<string>(cardBase.Keywords);
        this.KeywordPotency = new List<int>(cardBase.KeywordPotency);

        this.GridImage = cardBase.GridImage;
        this.Frame = cardBase.Frame;
    }

    public int GetKeywordValue(string keyword) //keyword
    {
        int index = Keywords.IndexOf(keyword);
        return KeywordPotency[index];
    }
}
