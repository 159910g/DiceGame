using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllAilments : MonoBehaviour
{
    //[SerializeField] List<GameObject> ailments;

    public static AllAilments Instance { get; private set; }

    AilmentsInterface[] ailments;

    public void Awake()
    {
        Instance = this;

        ailments = GetComponents<AilmentsInterface>();
    }

    public AilmentsInterface SearchAilments(string input)
    {
        for(int i=0; i < ailments.Length; i++)
        {
            if(ailments[i].AilmentName == input)
                return ailments[i];
        }

        return null;
    }

    public string SearchTriggerCondition(string input)
    {
        for(int i=0; i < ailments.Length; i++)
        {
            if(ailments[i].AilmentName == input)
                return ailments[i].TriggerCondition;
        }

        return null;
    }
}
