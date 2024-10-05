using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoBox : MonoBehaviour
{
    public static InfoBox Instance;
    [SerializeField] GameObject container; //Handles turning elements on/off
    [SerializeField] TextMeshProUGUI characterName;
    [SerializeField] TextMeshProUGUI characterHealth;
    [SerializeField] TextMeshProUGUI nextAction;
    [SerializeField] GameObject hoverableAilmentPrefab;
    [SerializeField] Vector3 topOfAilmentSection = new Vector3(0,0,0);
    [SerializeField] TextMeshProUGUI ailmentDetails;

    public event Action OnHideInfo;

    void Awake()
    {
        if (Instance is null)
        {
            Instance = this;
        }
    }

    private void FixedUpdate() {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            HideInfo();
        }
    }

    public void SetInfo(string charName, int currHealth, int maxHealth, EnemyAction action, Dictionary<AilmentsInterface, int> ailments)
    {
        HideInfo();
        container.SetActive(true);
        characterName.text = charName;
        characterHealth.text = "HP: " + currHealth + " / " + maxHealth;
        nextAction.text = "Next Action: " + action.ActionName + "\n";
        if (action.MaxATKValue > 0) 
        {
            nextAction.text += ("ATK: " + action.MinATKValue + "-" + action.MaxATKValue + " ");
        }
        if (action.MaxDEFValue > 0) 
        {
            nextAction.text += ("DEF: " + action.MinDEFValue + "-" + action.MaxDEFValue);
        }
        for(int i=0; i<action.Keywords.Count; i++)
        {
            nextAction.text += action.Keywords[i] + " " + action.MinKeywordPotency[i] + "-" + action.MaxKeywordPotency[i];
        }
        foreach (AilmentsInterface ailment in ailments.Keys)
        {
            Debug.Log(ailment);
            GameObject a = Instantiate(hoverableAilmentPrefab, topOfAilmentSection, Quaternion.identity);
            a.transform.parent = gameObject.transform;
            a.GetComponent<AilmentHover>().ailment = ailment;
            topOfAilmentSection = new Vector3(topOfAilmentSection.x, topOfAilmentSection.y - 50.0f, topOfAilmentSection.z);
        }
    }

    public void HideInfo()
    {
        ailmentDetails.text = "";
        container.SetActive(false);
        OnHideInfo?.Invoke();
    }

    public void ShowAilmentDetails(AilmentsInterface ailment)
    {
        ailmentDetails.text = ailment.AilmentIcon + " " + ailment.AilmentName + "\n\n" + ailment.AilmentDescription;
    }
}
