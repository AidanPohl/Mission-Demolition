/**
 * Created By: Aidan Pohl
 * Created: 02/02/2022
 * 
 * Last Edited By: N/A
 * Last Edited: N/A
 * 
 * Description: Follow Projectile
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

    public float camZ; //the desired Z pos of the Camera

    private void Awake()
    {
        camZ = this.transform.position.z;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (POI == null) return;//if no point of interest exit update
        Vector3 destination = POI.transform.position;
        destination.z = camZ;
        transform.position = destination;

    }//end FixedUpdate()
}
