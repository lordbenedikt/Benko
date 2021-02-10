using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    Animator Animation;
    public GameObject child;
    private void Start() {
        Animation = child.GetComponent<Animator>();
    }
    public void Idle(){
        Animation.SetInteger("current_pos", 0); //Idle Anim
    }
    public void Run(){
        Animation.SetInteger("current_pos", 1);   //Run    
    }
    public void Attack(){
        Animation.SetInteger("current_pos", 2); //Shoot Anim
    }
    public void Death(){
        Animation = GetComponent<Animator>();
        Animation.SetBool("dead", true);
    }
    
}
