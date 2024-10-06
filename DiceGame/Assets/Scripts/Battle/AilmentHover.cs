using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AilmentHover : MonoBehaviour
{
    public AilmentsInterface ailment;
    bool isHovered = false;

    void OnMouseOver()
    {
        if (!isHovered)
        {
            InfoBox.Instance.ShowAilmentDetails(ailment);
            isHovered = true;
        }
    }

    void OnMouseExit()
    {
        InfoBox.Instance.HideAilmentDetails();
        isHovered = false;
    }
}
