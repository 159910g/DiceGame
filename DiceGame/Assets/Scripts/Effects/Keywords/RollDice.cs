using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollDice : MonoBehaviour, KeywordInterface
{
    string keywordName = "Reroll";
    string keywordDescription = "Roll your Energy Dice again.";

    bool affectsTarget = false;

    KeywordTier tier = KeywordTier.Tier1;

    public string KeywordName { get => keywordName; }

    public string KeywordDescription { get => keywordDescription; }

    public KeywordTier Tier { get => tier; }

    public bool KeywordAffectsTarget { get => affectsTarget; }

    public void KeywordEffect(int potenecy)
    {
        BattleSystem.Instance.RollDice();
    }
}
