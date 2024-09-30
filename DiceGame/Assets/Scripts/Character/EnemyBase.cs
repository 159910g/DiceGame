using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Character/Create new Enemy")]

[System.Serializable]
public class EnemyBase : CharacterBase
{
    [SerializeField] List<EnemyAction> actions;
    [SerializeField] List<Sprite> actionIndicators;

    public List<EnemyAction> Actions
    {
        get { return actions; }
    }

    public List<Sprite> ActionIndicators
    {
        get { return actionIndicators; }
    }

}
