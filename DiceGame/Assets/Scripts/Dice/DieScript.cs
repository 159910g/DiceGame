using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DieScript : MonoBehaviour
{
    [SerializeField] DiceFaceBase[] faces;
    [SerializeField] TextMeshPro valueText;
    [SerializeField] SpriteRenderer bg;

    //called from battle system
    public void RollDie(System.Action<DieResult> onComplete)
    {
        int i = Random.Range(0, faces.Length - 1);

        // Start the coroutine and pass the callback
        //function(parameter, () =>
        //{
        //  STUFF THAT HAPPENS AFTER FUNCTION IS DONE
        //}
        StartCoroutine(DiceAnimation(i, () => 
        {
            // Update valueText and bg.sprite after animation finishes
            valueText.text = Mathf.Max(faces[i].ATKEnergy, faces[i].DEFEnergy, faces[i].UTLEnergy).ToString();
            bg.sprite = faces[i].DieBG;

            // Call the onComplete callback with the DieResult
            DieResult result = new DieResult(
                faces[i].ATKEnergy,
                faces[i].DEFEnergy,
                faces[i].UTLEnergy,
                faces[i].Effect,
                faces[i].EffectPotency
            );

            onComplete?.Invoke(result); // Invoke the callback with the result
        }));
    }

IEnumerator DiceAnimation(int rolledResult, System.Action onComplete)
{
    valueText.enabled = true;
    bg.enabled = true;

    for (int i = 0; i < (((faces.Length) * 3) + rolledResult + 1); i++)
    {
        int index = i % faces.Length;

        valueText.text = Mathf.Max(faces[index].ATKEnergy, faces[index].DEFEnergy, faces[index].UTLEnergy).ToString();
        bg.sprite = faces[index].DieBG;

        yield return new WaitForSeconds(0.01f * (i + 1));
    }

    yield return new WaitForSeconds(1f);
    valueText.enabled = false;
    bg.enabled = false;

    onComplete?.Invoke(); // Call the callback after the animation finishes
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
