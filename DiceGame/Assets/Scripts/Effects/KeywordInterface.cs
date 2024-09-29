using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//called in game, cards hold a reference to them
public interface KeywordInterface
{
    string KeywordName { get; }  // Property for the keyword's name

    string KeywordDescription { get; }  // Property for the keyword's description

    KeywordTier Tier { get; }

    bool KeywordAffectsTarget { get; }
    
    //potenecy value = 0 when keyword does not require potency
    void KeywordEffect(int potenecy);
}

[System.Serializable]
//differeniated by colour
public enum KeywordTier
{
    Tier1,
    Tier2,
    Tier3,
}
