using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AilmentHover : MonoBehaviour
{
    public AilmentsInterface ailment;

    void OnMouseOver()
    {
        InfoBox.Instance.ShowAilmentDetails(ailment);
    }
}
