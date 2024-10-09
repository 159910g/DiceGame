using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public static SceneManagement Instance;
    private void Start() 
    {
        if (Instance == null) Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void CallLoadScene(EncounterBase encounter)
    {
        StartCoroutine(LoadAsyncScene(encounter.BaseScene));
    }

    void Update()
    {
        // Press the space key to start coroutine
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Use a coroutine to load the Scene in the background
            StartCoroutine(LoadAsyncScene("BattleScene"));
        }
    }

    IEnumerator LoadAsyncScene(string sceneName)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.
        foreach (Scene s in SceneManager.GetAllScenes())
        {
            if (s.name != "MasterScene")
            {
                SceneManager.UnloadScene(s);
            }
        }
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
