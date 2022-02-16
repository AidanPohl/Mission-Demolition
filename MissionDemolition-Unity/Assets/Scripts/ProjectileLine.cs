/**
 * Created By: Aidan Pohl
 * Created: 02/16/2022
 * 
 * Last Edited N/A
 * Last Edited: N/A
 * Description: Tracking line
 * 
 * */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLine : MonoBehaviour  
{/** VARIABLES**/
    static public ProjectileLine S; 
    [Header("Set in Inspector")]
    public float minDist = 0.1f;

    private LineRenderer line;
    private GameObject _poi;
    private List<Vector3> points;

    private void Awake()
    {
        S = this;
        line = GetComponent<LineRenderer>();
        line.enabled = false;
        points = new List<Vector3>();
    }//end Awake()

    public GameObject poi{
        get{
            return (_poi);
        }//end get
        set{
            _poi = value;
            if (_poi != null)
            {
                line.enabled = false;
                points = new List<Vector3>();
                AddPoints();
            }//end if (_poi != null)
        }//end set
    }//end poi

    public void Clear()
    {
        _poi = null;
        line.enabled = false;
        points = new List<Vector3>();
    }//end Clear()
    public void AddPoints()
    {
        Vector3 pt = _poi.transform.position;

        if (points.Count > 0 && (pt - lastPoint).magnitude < minDist)
        {
            return;  
        }
        if (points.Count == 0)
        {
            Vector3 launchPosDiff = pt -Slingshot.LAUNCH_POS;
            points.Add(pt + launchPosDiff);
            points.Add(pt);
            line.positionCount = 2;
            line.SetPosition(0, points[0]);
            line.SetPosition(1, points[1]);
            line.enabled = true;
        }else{
            points.Add(pt);
            line.positionCount = points.Count;
            line.SetPosition(points.Count - 1, lastPoint);
            line.enabled = true;

        }//end if else

    }//end AddPoints()

    public Vector3 lastPoint
    {
        get
        {
            if (points == null)
            {
                // If there are no points, returns Vector3.zero
                return (Vector3.zero);
            }//end if (points == null)
            return (points[points.Count - 1]);
        }//end get()
    }//end lastPoint
    

   
    void FixedUpdate()
    {
        if (poi == null)
        {
            if (FollowCam.POI != null)
            {
                if (FollowCam.POI.tag == "Projectile")
                {
                        poi = FollowCam.POI;
                } else { return; }
            }
            else
            {
                return;
            }//end if else
        }//end if (poi == null) - else
        AddPoints();
       if (FollowCam.POI == null) { 
            poi = null;
        }//end if (FollowCam.POI == null)

    }//end FixedUpdate()
}
