using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotificationPopUp : MonoBehaviour
{
    [SerializeField] TextMeshPro Desc;

    public void SetPopUpData(string desc)
    {
        Desc.text = desc;
    }
}
