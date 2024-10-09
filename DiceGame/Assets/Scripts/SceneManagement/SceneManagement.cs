using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public static SceneManagement Instance;
    public EncounterBase currentEncounter;
    private void Start() 
    {
        if (Instance == null) Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void CallLoadScene(EncounterBase encounter)
    {
        StartCoroutine(LoadAsyncScene(encounter));
    }

    IEnumerator LoadAsyncScene(EncounterBase encounter)
    {
    // The Application loads the Scene in the background as the current Scene runs.
        currentEncounter = encounter;
        foreach (Scene s in SceneManager.GetAllScenes())
        {
            if (s.name != "MasterScene")
            {
                SceneManager.UnloadScene(s);
            }
        }
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(encounter.BaseScene, LoadSceneMode.Additive);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
