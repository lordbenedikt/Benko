using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public Canvas canvas;
    public GameObject selection;
    public GameObject wallPreviewPrefab;

    [HideInInspector]
    public UI_Manager UI;
    [HideInInspector]
    public CustomGrid customGrid;
    
    [HideInInspector]
    public GameObject wallPreview;

    void Awake()
    {
        UI = canvas.GetComponent<UI_Manager>();
        customGrid = gameObject.GetComponent<CustomGrid>();
        // print(customGrid);
        selection.SetActive(false);
        wallPreview = GameObject.Instantiate(wallPreviewPrefab, transform.position, Quaternion.Euler(0,0,0));
        wallPreview.SetActive(false);
    }

    void Update()
    {
        
    }

    public int gridIndexFromPos(float x, float z) {
        // print(customGrid);
        if (customGrid != null)
            return customGrid.gridIndexFromPos(x,z);
        return 0;
    }
}
