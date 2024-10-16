using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class GameController : MonoBehaviour
{

    #region Fields

    // References
    public GameObject InGameUiPrefab;
    public GameObject WallPreviewPrefab;
    public Transform UnitsSpawnPoint;    
    public GameObject SelectionIndicatorPrefab;

    // Enemy Spawning (via timeline)
    public string EnemySpawnTimelinePath = "Assets/Resources/enemy_spawn_timeline.json";
    public List<GameObject> LoadablePrefabSet;

    // Properties
    public Canvas InGameUi {get; private set;}
    public UI_Manager UiManager { get; private set; }
    public GameObject WallPreview { get; private set; }
    public List<EnemySpawnData> EnemySpawns { get; } = new List<EnemySpawnData>();
    public GameObject SelectionIndicator { get; private set; }


    // Private fields
    private CustomGrid customGrid;
    private List<EnemySpawnData> enemySpawnHistory = new List<EnemySpawnData>();
    private int spawnCounter = 0;


    #endregion

    #region Built-in Methods

    void Awake()
    {
        InGameUi = Instantiate(InGameUiPrefab, transform.position, Quaternion.Euler(0, 0, 0)).GetComponent<Canvas>();
        UiManager = InGameUi.GetComponent<UI_Manager>();
        customGrid = gameObject.GetComponent<CustomGrid>();
        WallPreview = Instantiate(WallPreviewPrefab, transform.position, Quaternion.Euler(0, 0, 0));
        WallPreview.SetActive(false);
        SelectionIndicator = Instantiate(SelectionIndicatorPrefab, transform.position, Quaternion.Euler(0, 0, 0));

        ReadStringToTimeline(EnemySpawnTimelinePath);
    }

    void Update()
    {
        SpawnEnemies();
        if (Input.GetKeyDown(KeyCode.P))
        {
            string json = "";
            foreach (EnemySpawnData data in enemySpawnHistory)
            {
                json += JsonUtility.ToJson(data) + "\n";
            }
            WriteString(json);
        }

        //controller.enemySpawnHistory.Add(new EnemySpawnData(Time.frameCount, prefabSetIndex, transform.position));

    }

    #endregion

    #region Custom Methods

    void SpawnEnemies()
    {
        while (spawnCounter < EnemySpawns.Count)
        {
            EnemySpawnData data = EnemySpawns[spawnCounter];
            if (data.frameCount < Time.frameCount)
            {
                Instantiate(LoadablePrefabSet[data.enemyPrefab], data.position, Quaternion.identity);
                spawnCounter++;
                continue;
            }
            break;
        }
    }


    public int GridIndexFromPos(float x, float z)
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

    /// <summary>
    ///     Reads enemy spawn data from file at <paramref name="path"/> and stores it in <see cref="EnemySpawns"/>.
    /// </summary>
    /// <param name="path"></param>
    void ReadStringToTimeline(string path)
    {
        if (EnemySpawnTimelinePath is null || EnemySpawnTimelinePath.Trim().Length == 0)
            return;

        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        EnemySpawns.Clear();
        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            EnemySpawns.Add(JsonUtility.FromJson<EnemySpawnData>(line));
        }
        reader.Close();
    }

    #endregion
}
