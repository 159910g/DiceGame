using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllKeywords : MonoBehaviour
{
    //[SerializeField] List<GameObject> keywords;

    public static AllKeywords Instance { get; private set; }

    KeywordInterface[] keywords;

    public void Awake()
    {
        Instance = this;

        keywords = GetComponents<KeywordInterface>();
    }

    public bool KeywordAffectsTarget(string input)
    {
        for(int i=0; i < keywords.Length; i++)
        {
            if(keywords[i].KeywordName == input)
                return keywords[i].KeywordAffectsTarget;
        }

        return false;
    }

    public void UseKeywordEffect(string input, int potency)
    {
        for(int i=0; i < keywords.Length; i++)
        {
            if(keywords[i].KeywordName == input)
                keywords[i].KeywordEffect(potency);
        }
    }

    public string SearchKeywords(string input)
    {
        for(int i=0; i < keywords.Length; i++)
        {
            if(keywords[i].KeywordName == input)
                return keywords[i].KeywordDescription;
        }

        return "Keyword Not Found";
    }
}
