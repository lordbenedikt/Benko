using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class GameController : MonoBehaviour
{

    public Canvas canvas;
    public GameObject selection;
    public GameObject wallPreviewPrefab;
    public string timelinePath = null;

    [HideInInspector]
    public UI_Manager UI;
    [HideInInspector]
    public CustomGrid customGrid;
    public List<GameObject> LoadablePrefabSet;
    [HideInInspector]
    public GameObject wallPreview;
    [HideInInspector]
    public List<EnemySpawnData> enemySpawns = new List<EnemySpawnData>();
    [HideInInspector]
    public List<EnemySpawnData> enemySpawnHistory = new List<EnemySpawnData>();
    private int spawnCounter = 0;

    void Awake()
    {
        UI = canvas.GetComponent<UI_Manager>();
        customGrid = gameObject.GetComponent<CustomGrid>();
        // print(customGrid);
        selection.SetActive(false);
        wallPreview = GameObject.Instantiate(wallPreviewPrefab, transform.position, Quaternion.Euler(0, 0, 0));
        wallPreview.SetActive(false);
        if (timelinePath != null) {
            ReadStringToTimeline(timelinePath);
        }
    }

    void Update()
    {
        while(spawnCounter<enemySpawns.Count) {
            EnemySpawnData data = enemySpawns[spawnCounter];
            if(data.frameCount < Time.frameCount) {
                GameObject go = Instantiate(LoadablePrefabSet[data.enemyPrefab], data.position, Quaternion.identity);
                spawnCounter++;
                continue;
            }
            break;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            string json = "";
            foreach (EnemySpawnData data in enemySpawnHistory)
            {
                json += JsonUtility.ToJson(data) + "\n";
            }
            WriteString(json);
        }
    }

    public int gridIndexFromPos(float x, float z)
    {
        // print(customGrid);
        if (customGrid != null)
            return customGrid.gridIndexFromPos(x, z);
        return 0;
    }

    static void WriteString(string str)
    {
        string path = "Assets/Resources/test.txt";

        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, false);
        writer.Write(str);
        writer.Close();

        //Re-import the file to update the reference in the editor
        AssetDatabase.ImportAsset(path);
        TextAsset asset = (TextAsset)Resources.Load("test");

        //Print the text from the file
        Debug.Log(asset.text);
    }
    void ReadStringToTimeline(string path)
    {
        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);

        enemySpawns.Clear();
        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            enemySpawns.Add(JsonUtility.FromJson<EnemySpawnData>(line));
        }
        reader.Close();
    }
}
