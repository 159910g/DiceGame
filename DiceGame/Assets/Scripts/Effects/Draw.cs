using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour, KeywordInterface
{
    string keywordName = "Draw";
    string keywordDescription = "Draw X cards from your deck";

    KeywordTier tier = KeywordTier.Tier1;

    public string KeywordName { get => keywordName; }

    public string KeywordDescription { get => keywordDescription; }

    public KeywordTier Tier { get => tier; }

    public void KeywordEffect(int potenecy)
    {
        //add stacks of "bleed" to opponent
    }
}
