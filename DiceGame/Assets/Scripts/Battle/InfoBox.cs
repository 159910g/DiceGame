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
    [SerializeField] TextMeshProUGUI characterAilments;
    [SerializeField] GameObject hoverableAilmentPrefab;
    [SerializeField] Vector3 topOfAilmentSection = new Vector3(0,0,0);

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

    public void SetInfo(string charName, int currHealth, int maxHealth, EnemyAction action=null, Dictionary<AilmentsInterface, int> ailments=null)
    {
        HideInfo();
        container.SetActive(true);
        characterName.text = charName;
        characterHealth.text = "HP: " + currHealth + " / " + maxHealth;
        nextAction.text = action.ActionName;
        foreach (AilmentsInterface ailment in ailments.Keys)
        {
            Instantiate(hoverableAilmentPrefab, topOfAilmentSection, Quaternion.identity);
            topOfAilmentSection = new Vector3(topOfAilmentSection.x, topOfAilmentSection.y - 50.0f, topOfAilmentSection.z);
        }
    }

    public void HideInfo()
    {
        container.SetActive(false);
        OnHideInfo?.Invoke();
    }
}
