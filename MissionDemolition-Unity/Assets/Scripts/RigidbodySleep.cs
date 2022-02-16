/**
 * Created By: Aidan Pohl
 * Created: 02/16/2022
 * 
 * Last Edited N/A
 * Last Edited: N/A
 * Description: Temporarily disables Rigidbody
 * */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodySleep : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //if this GameObject has a ridigbody, disables it for the first update
        Rigidbody rb = GetComponent<Rigidbody>();
        if(rb!=null) rb.Sleep();
    }//end Start()
}
