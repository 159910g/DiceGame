using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bleed : MonoBehaviour, KeywordInterface
{
    string keywordName = "Bleed";
    string keywordDescription = "Add X stacks of Bleed to Target";

    bool affectsTarget = true;

    KeywordTier tier = KeywordTier.Tier1;

    public string KeywordName { get => keywordName; }

    public string KeywordDescription { get => keywordDescription; }

    public KeywordTier Tier { get => tier; }

    public bool KeywordAffectsTarget { get => affectsTarget; }

    public void KeywordEffect(int potency, BattleCharacter target)
    {
        AilmentsInterface ai = AllAilments.Instance.SearchAilments(keywordName);
        if (target.statusAilments.ContainsKey(ai))
        {
            target.statusAilments[ai] += potency;
        }
        else
        {
            target.statusAilments.Add(ai, potency);
        }
    }
}
