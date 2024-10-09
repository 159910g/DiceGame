using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Encounter", menuName = "Encounter/Create new Encounter")]

[System.Serializable]
public class EncounterBase : ScriptableObject
{
    [SerializeField] string baseScene;
    [SerializeField] Sprite backgroundImage;
    // Allow for things like music, etc that exists in all scenes

    public string BaseScene
    {
        get { return baseScene; }
    }

    public Sprite BackgroundImage
    {
        get { return backgroundImage; }
    }
}

