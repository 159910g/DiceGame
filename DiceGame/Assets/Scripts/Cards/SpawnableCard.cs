using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class SpawnableCard : MonoBehaviour
{
    public Card card;
    public TextMeshPro Name;
    public TextMeshPro ATKEnergyCost;
    public TextMeshPro DEFEnergyCost;
    public TextMeshPro UTLEnergyCost;

    public TextMeshPro ATKValue;
    public TextMeshPro DEFValue;

    public List<TextMeshPro> Keywords;

    public SpriteRenderer Frame;
    public SpriteRenderer GridImage;
    public bool isHovered = false;
    public bool isSelected = false;
    public Vector3 startPos = new Vector3(0f, 0f, 0f);
    public Vector3 startScale = new Vector3(0f, 0f, 0f);


    void Start()
    {
        startPos = transform.position;
        startScale = transform.localScale;
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
        transform.DOMoveZ(startPos.z - 1f, 0.5f);
        transform.DOScale(startScale * 1.5f, 0.5f);
        yield return null;
    }

    public IEnumerator ReturnCard()
    {
        transform.DOMoveY(startPos.y, 0.5f);
        transform.DOMoveZ(startPos.z, 0.5f);
        transform.DOScale(startScale, 0.5f);
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
            if (isSelected) transform.DOMoveZ(startPos.z - 5f, 0.01f);
        }
    }
}
