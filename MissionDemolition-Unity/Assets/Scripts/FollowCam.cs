/**
 * Created By: Aidan Pohl
 * Created: 02/02/2022
 * 
 * Last Edited By: Aidan Pohl
 * Last Edited: 02/16/2022
 * 
 * Description: Camera Follow controls
 *
 * 
 * */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    /**VARIABLES**/
    static public GameObject POI; //statc point of interest

    [Header("Set in Inspecter")]
    public float easing = 0.5f; //amount of ease
    public Vector2 minXY = Vector2.zero;

    [Header("Set Dynamically")]
    public float camZ; //the desired Z pos of the Camera

    private void Awake()
    {
        camZ = this.transform.position.z;
    }

    private void FixedUpdate()
    {
        //if (POI == null) return;//if no point of interest exit update

        //Vector3 destination = POI.transform.position;

        Vector3 destination; //Destintion of POI

        if (POI == null)
        {
            destination = Vector3.zero;
        }
        else
        {
            destination = POI.transform.position;

            if(POI.tag == "Projectile")
            {   //if projectile is sleeping, set POI to null;
                if (POI.GetComponent<Rigidbody>().IsSleeping())
                {
                    POI = null;
                } //end if (POI.GetComponent<Rigidbody>().IsSleeping())
            }//end  if(POI.tag == "Projectile")
        }//end if else


        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);

        destination = Vector3.Lerp(transform.position, destination, easing);//interpolate from current camera position
                                                                            //towards destination

        destination.z = camZ; //reset the z of the destination to the camera z
        transform.position = destination; //set the position of the camera to the destination

        Camera.main.orthographicSize = destination.y + 10;

    }//end FixedUpdate()
}
