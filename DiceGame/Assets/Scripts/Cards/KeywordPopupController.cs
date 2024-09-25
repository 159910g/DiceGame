using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeywordPopupController : MonoBehaviour
{
    [SerializeField] KeywordPopup PopUp;

    KeywordPopup k;

    string keyword;
    int keywordPotency;

    public bool isHovered = false;

    public void Init(string k, int kp)
    {
        keyword = k;
        keywordPotency = kp;
    }

    public void OnMouseOver()
    {
        if (!isHovered)
        {
            Vector3 spawnLocation = new Vector3(transform.position.x + 50, transform.position.y + 150, transform.position.z);
            k = Instantiate(PopUp, spawnLocation, Quaternion.identity).GetComponent<KeywordPopup>();
            k.SetPopUpData(keyword, AllKeywords.Instance.SearchKeywords(keyword));
            isHovered = true;
        }
    }

    public void OnMouseExit()
    {
        if (isHovered)
        {
            Destroy(k.gameObject);
            isHovered = false;
        }
    }
}
