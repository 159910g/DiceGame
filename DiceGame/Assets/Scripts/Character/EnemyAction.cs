using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Action", menuName = "Action/Create new Action")]

[System.Serializable]
public class EnemyAction : ScriptableObject
{
    [SerializeField] string actionName;
    [SerializeField] string description;
    [SerializeField] int minAtkValue;
    [SerializeField] int maxAtkValue;
    [SerializeField] int minDefValue;
    [SerializeField] int maxDefValue;

    [SerializeField] Sprite actionIndicator;

    //1 2 3
    //4 5 6
    //7 8 9
    [SerializeField] List<bool> enemyTargets;
    [SerializeField] bool targetsPlayer;

    //keyword / value
    [SerializeField] List<string> keywords;
    [SerializeField] List<int> minKeywordPotency;
    [SerializeField] List<int> maxKeywordPotency;

    public string ActionName
    {
        get { return actionName; }
    }

    public string Description
    {
        get { return description; }
    }   

    public int MinATKValue
    {
        get { return minAtkValue; }
    }

    public int MaxATKValue
    {
        get { return maxAtkValue; }
    }

    public int MinDEFValue
    {
        get { return minDefValue; }
    }

    public int MaxDEFValue
    {
        get { return maxDefValue; }
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

    public List<int> MinKeywordPotency
    {
        get { return minKeywordPotency; }
    }

    public List<int> MaxKeywordPotency
    {
        get { return maxKeywordPotency; }
    }

    public Sprite ActionIndicator
    {
        get { return actionIndicator; }
    }
}
