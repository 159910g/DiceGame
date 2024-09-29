using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Action", menuName = "Action/Create new Action")]

[System.Serializable]
public class EnemyAction : ScriptableObject
{
    [SerializeField] string description;
    [SerializeField] int atkValue;
    [SerializeField] int defValue;
    //1 2 3
    //4 5 6
    //7 8 9
    [SerializeField] List<bool> enemyTargets;
    [SerializeField] bool targetsPlayer;

    //keyword / value
    [SerializeField] List<string> keywords;
    [SerializeField] List<int> keywordPotency;

    public string Description
    {
        get { return description; }
    }   

    public int ATKValue
    {
        get { return atkValue; }
    }

    public int DEFValue
    {
        get { return defValue; }
    }

    public List<bool> EnemyTargets
    {
        get { return enemyTargets; }
    }

    public bool TargetsPlayer
    {
        get { return targetsPlayer; }
    }

    public List<string> Keywords
    {
        get { return keywords; }
    }

    public List<int> KeywordPotency
    {
        get { return keywordPotency; }
    }

    public int GetKeywordValue(string keyword)
    {
        int index = keywords.IndexOf(keyword);
        return keywordPotency[index];
    }
}
