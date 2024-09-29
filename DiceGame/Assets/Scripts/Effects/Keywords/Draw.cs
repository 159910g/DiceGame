using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour, KeywordInterface
{
    string keywordName = "Draw";
    string keywordDescription = "Draw X cards from your deck";

    bool affectsTarget = false;

    KeywordTier tier = KeywordTier.Tier1;

    public string KeywordName { get => keywordName; }

    public string KeywordDescription { get => keywordDescription; }

    public bool KeywordAffectsTarget { get => affectsTarget; }

    public KeywordTier Tier { get => tier; }

    public void KeywordEffect(int potenecy)
    {
        for(int i = 0; i < potenecy; i++)
        {
            BattleSystem.Instance.Draw();
        }
    }
}
