using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnableCard : MonoBehaviour
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI ATKEnergyCost;
    public TextMeshProUGUI DEFEnergyCost;
    public TextMeshProUGUI UTLEnergyCost;

    public TextMeshProUGUI ATKValue;
    public TextMeshProUGUI DEFValue;

    public List<TextMeshProUGUI> Keywords;

    public SpriteRenderer Frame;
    public SpriteRenderer GridImage;
}
