using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetOffset : MonoBehaviour
{
    [SerializeField] int offsetValueX;
    [SerializeField] int offsetValueY;

    bool isHovered = false;
    public void OnMouseOver()
    {
        if (!isHovered)
        {
            Debug.Log(gameObject.name);
            TargetHandler.Instance.TargetOffset(offsetValueX,  offsetValueY);
            isHovered = true;
        }
    }

    public void OnMouseExit()
    {
        if (isHovered)
        {
            isHovered = false;
        }
    }
}
