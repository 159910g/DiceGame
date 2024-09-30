using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationPopupController : MonoBehaviour
{
    [SerializeField] NotificationPopUp PopUp;
    [SerializeField] BoxCollider2D BC;

    NotificationPopUp n;

    public void NotEnoughMana()
    {
        BattleSystem.Instance.state = BattleState.Busy;
        string output = "You do not have enough mana to play "+ BattleSystem.Instance.CardSelected.Name;
        n = Instantiate(PopUp, new Vector3(-200f,150f,0), Quaternion.identity).GetComponent<NotificationPopUp>();
        n.SetPopUpData(output);
        BC.enabled = true;
    }

    public void OnMouseDown()
    {
        if (n != null)
        {
            BC.enabled = false;
            Destroy(n.gameObject);
            BattleSystem.Instance.state = BattleState.PlayerTurn;
        }
    }
}
