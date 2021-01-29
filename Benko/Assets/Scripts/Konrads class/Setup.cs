using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public static class qwe 
{
    //public TextMeshPro TextMesh;

    // public static void TakeDamage(Transform _target, GameObject _popuptext, TextMeshPro TextMesh, float _damage){

    //     _target.gameObject.GetComponent<Health>().Currenthealth -= _damage;

    //     GameObject go = Instantiate(_popuptext, _target, Quaternion.identity);
    //     TextMesh = go.GetComponent<TextMeshPro>();
    //     TextMesh.SetText(_damage.ToString());
    //     //TextMesh
    //     Destroy(go,5.0f);
    //     print("yay");
    // }



    public static  object D(object v)
    {
        Debug.Log(v);
        return v;
       
    }

    public static float Step(float v)
    {
        return v * Time.deltaTime;
    }
}
