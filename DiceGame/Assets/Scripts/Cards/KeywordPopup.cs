using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeywordPopup : MonoBehaviour
{
    [SerializeField] TextMeshPro keywordName;
    [SerializeField] TextMeshPro keywordDesc;

    public void SetPopUpData(string name, string desc)
    {
        keywordName.text = name;
        keywordDesc.text = desc;
    }
}
