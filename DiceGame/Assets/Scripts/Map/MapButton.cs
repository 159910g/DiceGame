using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapButton : MonoBehaviour
{
    public List<MapButton> prerequisites = new List<MapButton>();
    public List<MapButton> antirequisites = new List<MapButton>();
    bool unlocked = false;
    bool completed = false;
    public EncounterBase encounterBase;

    void Start()
    {
        if (ProgressionManager.Instance.CompletedEncounters[this])
        {
            completed = true;
        }

        if (!completed && CheckPrerequisites() && CheckAntirequisites())
        {
            unlocked = true;
        }
        else
        {
            unlocked = false;
        }
    }

    public void LoadEncounter()
    {
        SceneManagement.Instance.CallLoadScene(encounterBase);
    }

    public bool CheckPrerequisites()
    {
        foreach (MapButton m in prerequisites)
        {
            if (ProgressionManager.Instance.CompletedEncounters[m] == false)
            {
                return false;
            }
        }
        return true;
    }

    public bool CheckAntirequisites()
    {
        foreach (MapButton m in prerequisites)
        {
            if (ProgressionManager.Instance.CompletedEncounters[m] == true)
            {
                return false;
            }
        }
        return true;
    }
}
