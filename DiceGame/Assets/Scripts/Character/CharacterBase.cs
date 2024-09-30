using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Character/Create new Character")]

[System.Serializable]
public class CharacterBase : ScriptableObject
{
    [SerializeField] string characterName;
    [SerializeField] int maxHP;
    [SerializeField] Sprite characterSprite;

    public int MaxHP
    {
        get { return maxHP; }
    }

    public string CharacterName
    {
        get { return characterName; }
    }

    public Sprite CharacterSprite
    {
        get { return characterSprite; }
    }
}
