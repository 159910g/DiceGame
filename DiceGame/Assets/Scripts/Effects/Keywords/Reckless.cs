using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reckless : MonoBehaviour, KeywordInterface
{
    string keywordName = "Reckless";
    string keywordDescription = "Take X Damage";

    bool affectsTarget = false;

    KeywordTier tier = KeywordTier.Tier1;

    public string KeywordName { get => keywordName; }

    public string KeywordDescription { get => keywordDescription; }

    public KeywordTier Tier { get => tier; }

    public bool KeywordAffectsTarget { get => affectsTarget; }

    public void KeywordEffect(int potency)
    {
        BattleSystem.Instance.player.TakeDamage(potency);
        Debug.Log($"Reckless Damage: {potency} applied to player");
    }
}
