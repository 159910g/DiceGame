using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
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

    public int GetKeywordValue(string keyword) //keyword
    {
        int index = Keywords.IndexOf(keyword);
        return KeywordPotency[index];
    }
}
