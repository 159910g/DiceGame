using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Character/Create new Enemy")]

[System.Serializable]
public class EnemyBase : CharacterBase
{
    [SerializeField] Sprite enemySprite;
    [SerializeField] List<EnemyAction> actions;
    [SerializeField] List<Sprite> actionIndicators;
    
    public Sprite EnemySprite
    {
        get { return enemySprite; }
    }

    public List<EnemyAction> Actions
    {
        get { return actions; }
    }

    public List<Sprite> ActionIndicators
    {
        get { return actionIndicators; }
    }

}
