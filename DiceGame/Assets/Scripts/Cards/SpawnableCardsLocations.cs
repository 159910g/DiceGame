using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableCardsLocations : MonoBehaviour
{
    [SerializeField] Vector2 SpawnLocationAnchor;
    [SerializeField] int maxSpace;

    public static SpawnableCardsLocations Instance { get; private set; }

    public void Awake()
    {
        Instance = this;
    }

    //get avg space, for setting cards in hand
    public List<Vector2> SpawnLocations(int numberOfCards)
    {
        List<Vector2> spawnLocations = new List<Vector2>();
        
        int spaceBetween = maxSpace / numberOfCards;

        for(int i=0; i < numberOfCards; i++)
        {
            float Xvalue = SpawnLocationAnchor.x + ( spaceBetween * i);
            Vector2 cord = new Vector2(Xvalue, SpawnLocationAnchor.y);
            spawnLocations.Add(cord);
        }
        
        return spawnLocations;
    }

    public void ReorientCardsInHand(List<SpawnableCard> cardsInHand)
    {
        if(cardsInHand.Count > 0)
        {
            List<Vector2> spawnLocations =  SpawnLocations(cardsInHand.Count);

            for(int i=0; i < cardsInHand.Count; i++)
            {
                //Z component visually layers cards with the rightmost on top
                Debug.Log(1f-(0.1f * i));
                cardsInHand[i].transform.position = new Vector3(spawnLocations[i].x, spawnLocations[i].y, 1f-(0.1f * i));
            }
        }
    }
}
