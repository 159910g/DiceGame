using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapButton : MonoBehaviour
{
    public List<MapButton> prerequisites = new List<MapButton>();
    public List<MapButton> antirequisites = new List<MapButton>();
    bool unlocked = false;
    bool completed = false;

    void Awake()
    {
        if (ProgressionManager.Instance.completedEncounters[this] == true)
        {
            completed = true;
        }
    }

    void Start()
    {
        if (!completed && CheckPrerequisites() && CheckAntirequisites())
        {
            unlocked = true;
        }
        else
        {
            unlocked = false;
        }
    }

    public bool CheckPrerequisites()
    {
        foreach (MapButton m in prerequisites)
        {
            if (ProgressionManager.Instance.completedEncounters[m] == false)
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
            if (ProgressionManager.Instance.completedEncounters[m] == true)
            {
                return false;
            }
        }

        return true;
    }
}
