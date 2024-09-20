using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCharacter : MonoBehaviour
{
    public int currentHP;
    public CharacterBase characterBase;

    public Dictionary<AilmentsInterface, int> statusAilments = new Dictionary<AilmentsInterface, int>(); 

    public void SetData()
    {
        currentHP = characterBase.MaxHP;
    }
}
