using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    Animator Animation;
    private void Start() {
        Animation = GetComponent<Animator>();
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
        Animation.SetBool("dead", true);
    }
    
}
