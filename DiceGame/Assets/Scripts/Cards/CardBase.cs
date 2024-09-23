using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Card/Create new Card")]

[System.Serializable]
public class CardBase : ScriptableObject
{
    //frame, grid image
    [SerializeField] int atkEnergyCost;

    [SerializeField] int defEnergyCost;

    [SerializeField] int utlEnergyCost;

    [SerializeField] int atkValue;

    [SerializeField] int defValue;

    //keyword / value
    [SerializeField] List<string> keywords;
    [SerializeField] List<int> keywordPotency;

    //1 2 3
    //4 5 6
    //7 8 9
    [SerializeField] List<bool> targets;

    [SerializeField] Sprite frame;

    [SerializeField] Sprite gridImage;

    public int ATKEnergyCost
    {
        get { return atkEnergyCost; }
    }

    public int DEFEnergyCost
    {
        get { return defEnergyCost; }
    }

    public int UTLEnergyCost
    {
        get { return utlEnergyCost; }
    }
    
    public int ATKValue
    {
        get { return atkValue; }
    }

    public int DEFValue
    {
        get { return defValue; }
    }

    public List<bool> Targets
    {
        get { return targets; }
    }

    public List<string> Keywords
    {
        get { return keywords; }
    }

    public List<int> KeywordPotency
    {
        get { return keywordPotency; }
    }

    public Sprite Frame
    {
        get { return frame; }
    }

    public Sprite GridImage
    {
        get { return gridImage; }
    }

    public int GetKeywordValue(string keyword) //keyword
    {
        int index = keywords.IndexOf(keyword);
        return keywordPotency[index];
    }
}
