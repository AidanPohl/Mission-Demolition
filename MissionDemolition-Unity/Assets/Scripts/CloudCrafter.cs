/**
 * Created By: Aidan Pohl
 * Created: 02/14/2022
 * 
 * Last Edited By: Aidan Pohl
 * Last Edited: 2/16/2022
 * Description: create clouds for scene
 * *//**
 * Created By: Aidan Pohl
 * Created: 02/14/2022
 * 
 * Last Edited By: Aidan Pohl
 * Last Edited: 2/16/2022
 * Description: create clouds for scene
 * */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudCrafter : MonoBehaviour
{
    /****Variables****/
    [Header("Set in Inspecter")]

    public int numClouds = 40;
    public GameObject cloudPrefab;
    public Vector3 cloudPosMin = new Vector3(-50, -5, 10);
    public Vector3 cloudPosMax = new Vector3(150, 100, 10);
    public float cloudScaleMin = 1f;
    public float cloudScaleMax = 3f;
    public float cloudSpeedMult = 0.5f;

    private GameObject[] cloudInstances;

    private void Awake()
    {
        cloudInstances = new GameObject[numClouds];
        GameObject anchor = GameObject.Find("Cloud Anchor");
        GameObject cloud;
        for( int i = 0; i < numClouds; i++)
        {
            cloud = Instantiate<GameObject>(cloudPrefab);

            //position cloud
            Vector3 cPos = Vector3.zero;
            cPos.x = Random.Range(cloudPosMin.x, cloudPosMax.x);
            cPos.y = Random.Range(cloudPosMin.y, cloudPosMax.y);

            //Scale Clouds
            float scaleU = Random.value;
            float scaleVal = Mathf.Lerp(cloudScaleMin, cloudScaleMax, scaleU);

            cPos.y = Mathf.Lerp(cloudPosMin.y, cPos.y, scaleU); //smaller clouds with smaller scale closer to ground

            cPos.z = 100 - 90 * scaleU; //push clouds into background

            //set adjustments
            cloud.transform.localScale *= scaleVal;
            cloud.transform.position = cPos;

            cloudInstances[i] = cloud; //add the created cloud to the list
        }//end for
    }//end Awake()

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject cloud in cloudInstances)
        {
            float scaleVal = cloud.transform.localScale.x;
            Vector3 cPos = cloud.transform.position;
            cPos.x -= scaleVal * Time.deltaTime * cloudSpeedMult;

            if (cPos.x <= cloudPosMin.x)
            {
                // Move it to the far right
                cPos.x = cloudPosMax.x;
            }
            // Apply the new position to cloud
            cloud.transform.position = cPos;
        }//end foreach
    }//end Update()
}
