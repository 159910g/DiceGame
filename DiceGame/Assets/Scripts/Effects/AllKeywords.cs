using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllKeywords : MonoBehaviour
{
    [SerializeField] List<GameObject> keywords;

    public static AllKeywords Instance { get; private set; }

    public void Awake()
    {
        Instance = this;
    }

    public string SearchKeywords(string input)
    {
        for(int i=0; i < keywords.Count; i++)
        {
            if(keywords[i].GetComponent<KeywordInterface>().KeywordName == input)
                return keywords[i].GetComponent<KeywordInterface>().KeywordDescription;
        }

        return "Keyword Not Found";
    }
}
