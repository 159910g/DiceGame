using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Battle", menuName = "Encounter/Create new Battle")]

[System.Serializable]
public class BattleBase : EncounterBase
{
    [SerializeField] EnemyBase[] enemies;

    // Some sort of field condition? ex: all characters get poisoned

    public EnemyBase[] Enemies
    {
        get { return enemies; }
    }
}
