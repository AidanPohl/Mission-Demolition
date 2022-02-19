/**
 * Created By: Aidan Pohl
 * Created: 02/19/2022
 * 
 * Last Edited By: N/A
 * Last Edited: N/A
 * 
 * Description: Game Manager
 *
 * 
 * */
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode{
    idle,
    playing,
    levelEnd
}

public class MissionDemolition : MonoBehaviour
{
    /*** VARIABLES ***/
    static private MissionDemolition S; //Singleton

    [Header ("Set In Inspector")]

    public Text uitLevel; //UIText_Level Text
    public Text uitShots; //UIText_shots Text
    public Text uitButton; //UIButton_View Text
    public Vector3 castlePos; //Castle Placement
    public GameObject[] castles; //Array of castles

    [Header("Set Dyamically")]

    public int level; //current level
    public int levelMax; //total number of levels
    public int shotsTaken;
    public GameObject castle; //current castle
    public GameMode mode = GameMode.idle;
    public string showing = "Show Slingshot"; ///FollowCam mode

    // Start is called before the first frame update
void Start()
    { S = this; //define Singleton

    level = 0;
    levelMax = castles.Length;
    StartLevel();
    }

void StartLevel(){
    //getrid of old castles if one exist
    if (castle != null){
        Destroy(castle);
    }

    //Destroy old projectiles if they exists
    GameObject[] gos = GameObject.FindGameObjectsWithTag("Projectile");
    foreach (GameObject proj in gos){
        Destroy(proj);
    }

    //Instantiate the new castle
    castle = Instantiate<GameObject>(castles[level]);
    castle.transform.position = castlePos;
    shotsTaken = 0;

    //Reset Camera
    SwitchView("Show Both");
    ProjectileLine.S.Clear();

    //Reset the Goal
    Goal.goalMet = false;

    UpdateGUI();
    mode = GameMode.playing;
}//end StartLevel()

void UpdateGUI(){
    //Show the data in the GUITexts
    uitLevel.text = "Level "+(level+1)+" of "+levelMax;
    uitShots.text = "Shots Taken: "+shotsTaken;
}//end UpdateGUI()

void Update(){
    UpdateGUI();

    //check if level end
    if ((mode == GameMode.playing)&& Goal.goalMet){
        //change mode to stope end game chech
        mode = GameMode.levelEnd;
        //Zoom out
        SwitchView("Show Both");
        //Start the next level in two seconds
        Invoke("NextLevel",2f);
    }//end  if ((mode == GameMode.playing)&& Goal.goalMet)
}//end Update()

    void NextLevel() {
        level++;
        if (level == levelMax) {
            level = 0;
        }
        StartLevel();
    }//void NextLevel()

public void SwitchView(string eView = ""){
    if (eView == ""){
        eView = uitButton.text;
    }//end if (eview == "")

    showing = eView;

    switch(showing){
        case "Show Slingshot":
            FollowCam.POI = null;
            uitButton.text = "Show Castle";
            break;
        case "Show Castle":
            FollowCam.POI = S.castle;
            uitButton.text = "Show Both";
            break;
        case "Show Both":
            FollowCam.POI = GameObject.Find("View Both");
            uitButton.text = "Show Slingshot";
            break;
    }// end switch(showing)
}//end SwitchView(eview)

// Static method that allows code anythwere to increment shotsTaken
public static void ShotFired(){
    S.shotsTaken++;
}//end ShotTaken()
}
