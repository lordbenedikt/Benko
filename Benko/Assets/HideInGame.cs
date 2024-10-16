using UnityEngine;

public class HideInGame : MonoBehaviour
{

    void Start()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

}
