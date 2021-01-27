using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public Canvas canvas;
    public GameObject selection;

    [HideInInspector]
    public UI_Manager UI;
    [HideInInspector]
    public CustomGrid customGrid;

    void Start()
    {
        UI = canvas.GetComponent<UI_Manager>();
        customGrid = gameObject.GetComponent<CustomGrid>();
        selection.SetActive(false);
    }

    void Update()
    {
        
    }

    public int gridIndexFromPos(float x, float z) {
        return customGrid.gridIndexFromPos(x,z);
    }
}
