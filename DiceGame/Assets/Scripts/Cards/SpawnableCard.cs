using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class SpawnableCard : MonoBehaviour
{
    public Card card;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI ATKEnergyCost;
    public TextMeshProUGUI DEFEnergyCost;
    public TextMeshProUGUI UTLEnergyCost;

    public TextMeshProUGUI ATKValue;
    public TextMeshProUGUI DEFValue;

    public List<TextMeshProUGUI> Keywords;

    public SpriteRenderer Frame;
    public SpriteRenderer GridImage;
    public bool isHovered = false;
    public bool isSelected = false;
    public Vector3 startPos = new Vector3(0f, 0f, 0f);


    void Start()
    {
        startPos = transform.position;
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            Debug.Log("Pressed Right Click");
            isSelected = false;
            StartCoroutine(ReturnCard());
        }
    }

    public IEnumerator LookAtCard()
    {
        transform.DOMoveY(startPos.y + 40, 0.5f);
        transform.DOMoveZ(startPos.z - 10, 0.5f);
        yield return null;
    }

    public IEnumerator ReturnCard()
    {
        transform.DOMoveY(startPos.y, 0.5f);
        transform.DOMoveZ(startPos.z, 0.5f);
        yield return null;
    }

    public void OnMouseOver()
    {
        if (!isHovered)
        {
            StartCoroutine(LookAtCard());
            isHovered = true;
        }
    }

    public void OnMouseExit()
    {
        if (isHovered && !isSelected)
        {
            StartCoroutine(ReturnCard());
            isHovered = false;
        }
    }

    public void OnMouseDown()
    {
        if (!isSelected)
        {
            BattleSystem.Instance.SelectCard(this);
        }
    }
}
