using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Energy : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI atkText;
    [SerializeField] TextMeshProUGUI defText;
    [SerializeField] TextMeshProUGUI utlText;

    public int atkEnergy = 0;
    public int defEnergy = 0;
    public int utlEnergy = 0;

    public void ChangeEnergy(int atk, int def, int utl)
    {
        atkEnergy += atk;
        defEnergy += def;
        utlEnergy += utl;

        atkText.text = atkEnergy.ToString();
        defText.text = defEnergy.ToString();
        utlText.text = utlEnergy.ToString();
    }
}
