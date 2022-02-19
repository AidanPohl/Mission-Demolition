/**
 * Created By: Aidan Pohl
 * Created: 02/02/2022
 * 
 * Last Edited By: N/A
 * Last Edited: N/A
 * 
 * Description: Slingshot Controller
 * */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    /**VARIABLES**/
    [Header("Set in Inspecter")]
    public GameObject prefabProjectile;
    public float velocityMultiplier = 8f;

    [Header("Set Dynamically")]
    public GameObject launchpoint;
    public Vector3 launchPos;
    public GameObject projectile; //Instance of Projectile
    public bool aimingMode; //Is Player Aiming
    public Rigidbody projectileRB; //Rigid Body of Projectile

    static private Slingshot S;
    static public Vector3 LAUNCH_POS
    {                                        
        get
        {
            if (S == null) return Vector3.zero;
            return S.launchPos;
        }
    }
    private void Awake()
    {
        S = this;                                                             

        Transform launchPointTrans = transform.Find("Launch Point");
        launchpoint = launchPointTrans.gameObject;
        launchpoint.SetActive(false);
        launchPos = launchPointTrans.position;//position of launch point
    }//end Awake()

    // Update is called once per frame
    void Update()
    {
        if (!aimingMode) return; //if not aiming exit update

        //get mouse position from 2d coordinates
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        //check mouse position delta
        Vector3 mouseDelta = mousePos3D - launchPos;

        //limit the mouse delta to slingshot collider radius
        float maxMagnitude = this.GetComponent<SphereCollider>().radius;

        if(mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();//sets the vecotr to the same direction
                                   //but a length of 1
            mouseDelta *= maxMagnitude;

        }//end if(mouseDelta > maxMagnitude)

        //Move projectile to a new position
        Vector3 projectilePos = launchPos + mouseDelta;
        projectile.transform.position = projectilePos;

        if (Input.GetMouseButtonUp(0)) {
            //mouse button has been released
            aimingMode = false;
            projectileRB.isKinematic = false;
            projectileRB.velocity = -mouseDelta * velocityMultiplier;
            FollowCam.POI = projectile;
            projectile = null; //emptys reference to instancerprojectile
            MissionDemolition.ShotFired();
            ProjectileLine.S.poi = projectile;
        }//end if (Input.GetMouseButtonUp(0))
    } //end Update()

    private void OnMouseEnter()
    {
        //print("Slingshot: OnMouseEnter");
        launchpoint.SetActive(true);
    }//end OnMouseEnter()

    private void OnMouseExit()
    {
        //print("Slingshot: OnMouseExit");
        launchpoint.SetActive(false);
    }//end OnMouseExit()

    private void OnMouseDown()
    {
        aimingMode = true; //player is aiming
        projectile = Instantiate(prefabProjectile) as GameObject; //instantiate projectile GO
        projectile.transform.position = launchPos;
        projectileRB = projectile.GetComponent<Rigidbody>();
        projectileRB.isKinematic = true;

    }//end OnMouseDown;
}
