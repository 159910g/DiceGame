using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetHandler : MonoBehaviour
{
    public static TargetHandler Instance;
    [SerializeField] List<GameObject> enemyGrid;
    [SerializeField] GameObject player;

    List<bool> storedTargets;

    private void Start() {
        if (Instance == null)
        {
            Instance = this;
        }

        player.GetComponent<SpriteRenderer>().enabled = false;

        foreach(GameObject g in enemyGrid)
        {
            g.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void TargetPlayer()
    {
        player.GetComponent<SpriteRenderer>().enabled = true;
    }

    //functions gets called when a card is selected
    //turn off current targets
    //show new targets with an offset of 0,0 (meaning the grid will light up to match the card's image)
    public void SetTargets(List<bool> targets)
    {
        storedTargets = targets;
        TurnOffAllTargets();
        TargetOffset(0,0);
    }

    public void UpdateTargets(List<bool> targets)
    {
        Debug.Log(targets.Count);
        if(targets.Count < 10) //card targets enemies
        {
            //turn off player "hitbox"
            player.GetComponent<TargetOffset>().enabled = false;

            //turn on every enemies's "hitbox"
            for(int i = 0; i < 9; i++)
            {
                enemyGrid[i].GetComponent<TargetOffset>().enabled = true;

                if(targets[i])
                    enemyGrid[i].GetComponent<SpriteRenderer>().enabled = true;

                else
                    enemyGrid[i].GetComponent<SpriteRenderer>().enabled = false;
            }
        }
        else //card targets player
        {   
            //turn on player "hitbox"
            player.GetComponent<TargetOffset>().enabled = true;

            //turn off enemy "hitboxes"
            for(int i = 0; i < 9; i++)
            {
                enemyGrid[i].GetComponent<TargetOffset>().enabled = false;
            }

            //turn on player targetting grid
            player.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    public void TurnOffAllTargets()
    {
        List<bool> tempList = new List<bool>();
        for(int i = 0; i < enemyGrid.Count; i++)
        {
            tempList.Add(false);
        }
        player.GetComponent<SpriteRenderer>().enabled = false;
        UpdateTargets(tempList);
    }

    //offset
    public void TargetOffset(int offsetValueX, int offsetValueY)
    {
        Debug.Log(offsetValueX +","+ offsetValueY);

        List<bool> tempList = new List<bool>();
        for(int i = 0; i < enemyGrid.Count; i++)
        {
            tempList.Add(false); // Initialize tempList with 'false' values
        }

        // Create a 3x3 grid from storedTargets
        List<List<bool>> grid = new List<List<bool>>();
        for (int i = 0; i < 3; i++) // row
        {
            grid.Add(new List<bool>());
            for (int j = 0; j < 3; j++) // column
            {
                grid[i].Add(storedTargets[i * 3 + j]); // Fill the grid from storedTargets
            }
        }

        // Apply the offset and copy values back to tempList
        for (int i = 0; i < 3; i++) // row
        {
            for (int j = 0; j < 3; j++) // column
            {
                // Calculate the new indices after applying the offset
                //have no idea why row + offset y works but it does
                int newRow = i + offsetValueY;
                int newCol = j + offsetValueX;

                // Check if the new indices are within bounds
                if(newRow >= 0 && newRow < 3 && newCol >= 0 && newCol < 3)
                {
                    // Convert the new 2D indices to 1D index
                    int newIndex = newRow * 3 + newCol;
                    tempList[newIndex] = grid[i][j]; // Update tempList with the offset grid values
                }
            }
        }

        //only cards that target the player have a count of 10
        if(storedTargets.Count == 10)
        {
            tempList.Add(storedTargets[9]);
        }

        UpdateTargets(tempList);
    }

    public void ResolveCard(Card card)
    {
        if(card.DEFValue > 0)
        {
            player.GetComponentsInChildren<BattleCharacter>()[0].ChangeCurrentArmour(card.DEFValue);
        }

        for(int i = 0; i < enemyGrid.Count; i++)
        {
            if(enemyGrid[i].GetComponent<SpriteRenderer>().enabled)
                enemyGrid[i].GetComponent<TargetOffset>().ResolveCard(card);

        }

        if(player.GetComponent<TargetOffset>().enabled)
            player.GetComponent<TargetOffset>().ResolveCard(card);
    }
}
