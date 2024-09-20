using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dice", menuName = "Dice/Create new Dice Face")]

[System.Serializable]
public class DiceFaceBase : ScriptableObject
{
    [SerializeField] int atkEnergy;

    [SerializeField] int defEnergy;

    [SerializeField] int utlEnergy;

    [SerializeField] DiceEffects effect;

    [SerializeField] int effectPotency;

    public int ATKEnergy
    {
        get { return atkEnergy; }
    }

    public int DEFEnergy
    {
        get { return defEnergy; }
    }

    public int UTLEnergy
    {
        get { return utlEnergy; }
    }

    public DiceEffects Effect
    {
        get { return effect; }
    }
    public int EffectPotency
    {
        get { return effectPotency; }
    }
}

public enum DiceEffects
{
    None,
    ATKBuff,
    DEFBuff,

}
