using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBlock : MonoBehaviour
{
    public BattleCharacter character;

    public void OnMouseDown()
    {
        Debug.Log("Mouse Clicked");
        if (character != null)
        {
            Debug.Log("Calling showinfo() on " + character.name);
            character.ShowInfo();
        }
    }
}
