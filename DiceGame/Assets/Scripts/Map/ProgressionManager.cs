using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionManager : MonoBehaviour
{
    public Dictionary<MapButton, bool> completedEncounters = new Dictionary<MapButton, bool>();

    public static ProgressionManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void InitializeEncounters()
    {
        //Add all mapbuttons in the scene to the encounters list
        //Call this when a map is first created
        foreach (MapButton m in FindObjectsOfType(typeof(MapButton)))
        {
            completedEncounters[m] = false;
        }
    }

    public void CompleteLevel(MapButton m)
    {
        completedEncounters[m] = true;
    }
}
