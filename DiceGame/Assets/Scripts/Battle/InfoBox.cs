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
    [SerializeField] Vector3 startOfAilmentSection = new Vector3(0,0,0);
    [SerializeField] TextMeshProUGUI ailmentDetails;
    List<GameObject> currentHoverableAilments = new List<GameObject>();

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

    public void SetInfo(string charName, int currHealth, int maxHealth, EnemyAction action, Dictionary<AilmentsInterface, int> ailmentsPotency)
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
        float offsetX = 0f; //Track where next word should start
        float offsetY = 0f;
        foreach (AilmentsInterface ailment in ailmentsPotency.Keys)
        {
            Debug.Log(ailment);
            GameObject a = Instantiate(hoverableAilmentPrefab, startOfAilmentSection, Quaternion.identity);
            a.transform.parent = gameObject.transform;
            a.transform.localPosition = new Vector3(
                startOfAilmentSection.x + startOfAilmentSection.x + offsetX, 
                startOfAilmentSection.y + startOfAilmentSection.y + offsetY,
                startOfAilmentSection.z
            );
            TextMeshProUGUI ailmentText = a.GetComponent<TextMeshProUGUI>();
            ailmentText.text = ailment.AilmentName + ailmentsPotency[ailment];
            offsetX += ailmentText.GetRenderedValues(true).x;
            if (offsetX >= 292)
            {
                offsetX = -60; //Second and subsequent rows are more to the left
                offsetY -= 30;
            }
            a.GetComponent<AilmentHover>().ailment = ailment;
            currentHoverableAilments.Add(a);
        }
    }

    public void HideInfo()
    {
        foreach (GameObject ailment in currentHoverableAilments)
        {
            Destroy(ailment);
        }
        ailmentDetails.text = "";
        container.SetActive(false);
        OnHideInfo?.Invoke();
    }

    public void ShowAilmentDetails(AilmentsInterface ailment)
    {
        ailmentDetails.text = ailment.AilmentIcon + " " + ailment.AilmentName + "\n\n" + ailment.AilmentDescription;
    }

    public void HideAilmentDetails()
    {
        ailmentDetails.text = "";
    }
}
