using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Merchant", menuName = "Merchant/Create new Merchant")]

[System.Serializable]

public class MerchantBase : ScriptableObject
{
    [SerializeField] string name;
    [SerializeField] string greetings;
    [SerializeField] string byes;

    [SerializeField] List<CardBase> cards;

    [SerializeField] List<string> keywords;

    //items
    public string Name
    {
        get { return name; }
    }

    public string Greetings
    {
        get { return greetings; }
    }
    public string Byes
    {
        get { return byes; }
    }

    public List<CardBase> Cards
    {
        get { return cards; }
    }

    public List<string> Keywords
    {
        get { return keywords; }
    }
}
