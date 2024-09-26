using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DieScript : MonoBehaviour
{
    [SerializeField] DiceFaceBase[] faces;
    [SerializeField] TextMeshPro valueText;

    public DieResult RollDie()
    {
        int i = Random.Range(0, faces.Length - 1);

        ///Animation???///
        valueText.text = Mathf.Max(faces[i].ATKEnergy, faces[i].DEFEnergy, faces[i].UTLEnergy).ToString();

        return new DieResult(
            faces[i].ATKEnergy,
            faces[i].DEFEnergy,
            faces[i].UTLEnergy,
            faces[i].Effect,
            faces[i].EffectPotency
        );
    }
}

public class DieResult
{
    public int ATKEnergy = 0;
    public int DEFEnergy = 0;
    public int UTLEnergy = 0;
    public DiceEffects Effect = DiceEffects.None;
    public int EffectPotency = 0;

    public DieResult(int ATKEnergy, int DEFEnergy, int UTLEnergy, DiceEffects Effect, int EffectPotency)
    {
        this.ATKEnergy = ATKEnergy;
        this.DEFEnergy = DEFEnergy;
        this.UTLEnergy = UTLEnergy;
        this.Effect = Effect;
        this.EffectPotency = EffectPotency;
    }
}
