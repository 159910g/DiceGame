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
    public List<SpriteRenderer> GridImage;
    public bool isHovered = false;
    bool isSelected = false;
    public Vector3 startPos = new Vector3(0f, 0f, 0f);
    public Vector3 startScale = new Vector3(0f, 0f, 0f);

    public Sprite whiteSquare;
    public Sprite redSquare;
    public Sprite blueSquare;

    [SerializeField] BoxCollider2D bc;

    public bool IsSelected
    {
        get {return isSelected;}
    }


    void Start()
    {
        startPos = transform.position;
        startScale = transform.localScale;
    }

    public void Init(Card inputCard)
    {   
        card = inputCard;
        Name.text = inputCard.Name;

        ATKEnergyCost.text = inputCard.ATKEnergyCost.ToString();
        DEFEnergyCost.text = inputCard.DEFEnergyCost.ToString();
        UTLEnergyCost.text = inputCard.UTLEnergyCost.ToString();

        ATKValue.text = inputCard.ATKValue.ToString();
        DEFValue.text = inputCard.DEFValue.ToString();

        Frame.sprite = inputCard.Frame;

        //goes through all the keywords on the card and adds them then turns off excess keyword slots
        for(int i = 0; i < Keywords.Count; i++)
        {
            if(i < inputCard.Keywords.Count)
            {
                if(inputCard.GetKeywordValue(inputCard.Keywords[i]) > 0)
                    Keywords[i].text = inputCard.Keywords[i] +" : "+ inputCard.GetKeywordValue(inputCard.Keywords[i]);
                else
                    Keywords[i].text = inputCard.Keywords[i];
                    
                Keywords[i].gameObject.SetActive(true);
                Keywords[i].GetComponent<BoxCollider2D>().enabled = false;
                Keywords[i].GetComponent<KeywordPopupController>().Init(inputCard.Keywords[i], inputCard.GetKeywordValue(inputCard.Keywords[i]));
            }
            else
            {
                Keywords[i].gameObject.SetActive(false);
            }
        }

        //iterate through grid, if card targets a spot, turn that spot red otherwise make it white
        for(int i = 0; i < 9; i++)
        {
            if(inputCard.Targets[i] == true)
            {
                GridImage[i].sprite = redSquare;
            }
            else
            {
                GridImage[i].sprite = whiteSquare;
            }
        }

        if(inputCard.Targets.Count == 10)
        {
            if(inputCard.Targets[9] == true)
                {
                    GridImage[4].sprite = blueSquare;
                }
        }

    }

    //function needs to check if card is selected
    //iterate through the list of keywords, if keyword is set active: turn on its collider
    
    //this function has to get called in order to change isSelected
    //turns on keyword colliders when card is selected, turns them off when it is deselected
    public void SetIsSelected(bool value)
    {
        isSelected = value;
        
        if(value)
        {
            foreach(TextMeshPro k in Keywords)
            {
                if(k.gameObject.active)
                {
                    k.GetComponent<BoxCollider2D>().enabled = true;
                }
            }
        }

        else
        {
            foreach(TextMeshPro k in Keywords)
            {
                if(k.gameObject.active)
                {
                    k.GetComponent<BoxCollider2D>().enabled = false;
                }
            }
        }
    }

    void FixedUpdate()
    {
        if(BattleSystem.Instance != null)
        {
            if(bc.enabled)
            {
                if(BattleSystem.Instance.state != BattleState.PlayerTurn)   
                    bc.enabled = false;
            }
            if(!bc.enabled)
            {
                if(BattleSystem.Instance.state == BattleState.PlayerTurn)   
                    bc.enabled = true;
            }
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            DeselectCard();
        }
    }

    public void DeselectCard()
    {
        if(BattleSystem.Instance != null)
            BattleSystem.Instance.ClearCardSelected();

        Debug.Log("Pressed Right Click");
        SetIsSelected(false);
        StartCoroutine(ReturnCard());
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
        SetIsSelected(false);
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
            isHovered = false;
            StartCoroutine(ReturnCard());
        }
    }

    public void OnMouseDown()
    {
        if (!isSelected)
        {
            if(BattleSystem.Instance != null)
                BattleSystem.Instance.SelectCard(this);

            if(CardShop.Instance != null)
                CardShop.Instance.SelectCard(this);
                
            if (isSelected) transform.DOMoveZ(startPos.z - 5f, 0.01f);
        }
    }
}
