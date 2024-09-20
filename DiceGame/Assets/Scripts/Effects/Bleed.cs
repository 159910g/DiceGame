using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bleed : MonoBehaviour, KeywordInterface
{
    string keywordName = "Bleed";
    string keywordDescription = "Bleed's Description";

    KeywordTier tier = KeywordTier.Tier1;

    public string KeywordName { get => keywordName; }

    public string KeywordDescription { get => keywordDescription; }

    public KeywordTier Tier { get => tier; }

    public void KeywordEffect(int potenecy)
    {
        //add stacks of "bleed" to opponent
    }
}
