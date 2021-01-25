using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Functions
{
    public static MeshFilter getMeshByName(string name)
    { 
        MeshFilter[] meshes = Resources.FindObjectsOfTypeAll<MeshFilter>();

        for (int i = 0; i < meshes.Length; i++)
        {
            if (meshes[i].name == name)
            {
                MonoBehaviour.print("Hi");
                return meshes[i];
            }
        }
        MonoBehaviour.print("Bye");
        return null;
    }
}
