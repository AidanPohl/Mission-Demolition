/**
 * Created By: Aidan Pohl
 * Created: 02/14/2022
 * 
 * Last Edited By: N/A
 * Last Edited: N/A
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

            cPos.y = Mathf.Lerp(cloudPosMin.y, cPos.y, scaleVal); //smaller clouds with smaller scale closer to ground

            //set adjustments
            cloud.transform.localScale *= scaleVal;
            cloud.transform.position = cPos;
        }//end for
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
