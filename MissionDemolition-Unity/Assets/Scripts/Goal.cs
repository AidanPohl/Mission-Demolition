/**
 * Created By: Aidan Pohl
 * Created: 02/19/2022
 * 
 * Last Edited By: N/A
 * Last Edited: N/A
 * 
 * Description: Goal trigger handling
 *
 * 
 * */
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    /*** VARIABLES ***/
    public static bool goalMet = false;

    void OnTriggerEnter( Collider other){
        //check if the collision is from the projectile
        if(other.gameObject.tag == "Projectile"){
            Goal.goalMet = true;
            Material mat = GetComponent<Renderer>().material;
            Color c = mat.color;
            c.a = 1;
            mat.color = c;
        }//end if(other.GameObject.tag == "Projectile")
    }//end ontriggerEnter
}
