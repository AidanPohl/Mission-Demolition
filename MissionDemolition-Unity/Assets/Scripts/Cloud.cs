/**
 * Created By: Aidan Pohl
 * Created: 02/14/2022
 * 
 * Last Edited By: N/A
 * Last Edited: N/A
 * Description: randomly generate a cloud
 * */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour { 
      /**VARIABLES**/
    [Header("Set in Inspecter")]
    public GameObject cloudSphere;
    public int numSpheresMin = 6;
    public int numSpheresMax = 10;
    public Vector2 sphereScaleRangeX = new Vector2(4, 8);
    public Vector2 sphereScaleRangeY = new Vector2(2, 4);
    public Vector2 sphereScaleRangeZ = new Vector2(2, 4);
    public Vector3 sphereOffsetScale = new Vector3(5, 2, 1);
    public float scaleYMin = 2f;

    private List<GameObject> spheres;



    // Start is called before the first frame update
    void Start()
    {
        spheres = new List<GameObject>();

        int num = Random.Range(numSpheresMin, numSpheresMax);

        for (int i = 0; i < num; i++)
        {
            GameObject sphere = Instantiate<GameObject>(cloudSphere);
            spheres.Add(sphere);

            Transform spTrans = sphere.transform;

            spTrans.SetParent(this.transform);

            //Randomly assign a position
            Vector3 offset = Random.insideUnitSphere;
            offset.x *= sphereOffsetScale.x;
            offset.y *= sphereOffsetScale.y;
            offset.z *= sphereOffsetScale.z;

            spTrans.localPosition = offset;

            //Randomly assign scale
            Vector3 scale = Vector3.one;
            scale.x = Random.Range(sphereScaleRangeX.x, sphereScaleRangeX.y);
            scale.y = Random.Range(sphereScaleRangeY.x, sphereScaleRangeY.y);
            scale.z = Random.Range(sphereScaleRangeZ.x, sphereScaleRangeZ.y);

            //adjust y scale by x distance from origin
            scale.y *= 1 - (Mathf.Abs(offset.x) / sphereOffsetScale.x);
            scale.y = Mathf.Max(scale.y, scaleYMin);


            spTrans.localScale = scale;

        }//end for (int i = 0; i < num; i++)
    }

    // Update is called once per frame
    void Update()
    {
        //keypress spacebar input
       /** if (Input.GetKeyDown(KeyCode.Space))
       * {
       *     Restart();
       * }
       **/
    }

    void Restart()
    {
        foreach(GameObject sphere in spheres)
        {
            Destroy(sphere);
        }
        Start();
    }

}
