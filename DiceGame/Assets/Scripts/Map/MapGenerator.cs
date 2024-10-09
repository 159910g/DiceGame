using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] List<EncounterBase> possibleEncounters = new List<EncounterBase>();
    [SerializeField] EncounterBase startEncounter;
    [SerializeField] EncounterBase endBossEncounter;
    [SerializeField] GameObject mapButtonPrefab;
    [SerializeField] GameObject mapCanvas;
    [SerializeField] float rowOffset;
    [SerializeField] float columnOffset;
    public int numRows = 1;
    public int maxWidth = 3;
    public List<List<MapButton>> spaces = new List<List<MapButton>>();
    public MapButton currentSpace;
    public Vector3 v = new Vector3(0f, 0f, 0f);

    public void GenerateMap()
    {
        //Add starting space
        spaces.Add(new List<MapButton>());
        MapButton mapSpace = Instantiate(mapButtonPrefab, v, Quaternion.identity).GetComponent<MapButton>();
        mapSpace.gameObject.transform.SetParent(mapCanvas.transform);
        mapSpace.encounterBase = startEncounter;
        mapSpace.transform.localPosition = v;
        spaces[0].Add(mapSpace);

        for (int i=1; i<numRows-1; i++)
        {
            List<MapButton> row = new List<MapButton>();
            int rowWidth = Random.Range(1, maxWidth);
            for (int j=0; j<rowWidth; j++)
            {
                EncounterBase encounter = possibleEncounters[Random.Range(0, possibleEncounters.Count)];
                MapButton m = Instantiate(mapButtonPrefab, v, Quaternion.identity).GetComponent<MapButton>();
                m.gameObject.transform.SetParent(mapCanvas.transform);
                m.encounterBase = encounter;
                m.transform.localPosition = new Vector3(v.x + columnOffset * j, v.y + rowOffset * i, v.z);
                row.Add(m);
            }
            spaces.Add(row);
        }
    }

    void Awake()
    {
        GenerateMap();
    }
}
