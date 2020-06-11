using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using System.IO;
using Valve.VR.InteractionSystem;

public class Controller : MonoBehaviour
{
    public bool Run = true;

    public int Round = 0;

    List<List<Vector3>> Paths;
    List<Vector3> Path;
    VectorListBuilder pathBuilder;

    GameObject Player;
    GameObject Diamond;//sign of destination

    public GameObject NextRound;
    public GameObject Cross;
    bool DisplayNext = false;

    public GameObject Arrow; // single arrow load from prefab
    public GameObject ArrowInstance; // arrow line
    public bool BuildArrowLine = true; //control build once


    public GameObject Plane;
    float PlaneMoveSpeed = 2.5f;
    int WaypointIndex = 0;

    public GameObject HeadUp ;
    public float Distance = 1f;

    public string FilePath;
    public string SummaryFile;
    public int TaskID = 1;

    

    void Start()
    {
        Player = GameObject.Find("Player");
        Diamond = GameObject.Find("Diamond");

        PlayerPrefs.SetInt("Points", 0);
        PlayerPrefs.SetInt("Collision", 0);

        pathBuilder = new VectorListBuilder();
        Paths = new List<List<Vector3>>();
        Paths.Add(pathBuilder.Builder16());//0
        Paths.Add(pathBuilder.Builder17());
        Paths.Add(pathBuilder.Builder18());

        Paths.Add(pathBuilder.Builder13());//3
        Paths.Add(pathBuilder.Builder14());
        Paths.Add(pathBuilder.Builder15());

        Paths.Add(pathBuilder.Builder10());
        Paths.Add(pathBuilder.Builder11());
        Paths.Add(pathBuilder.Builder12());

        Paths.Add(pathBuilder.Builder7());
        Paths.Add(pathBuilder.Builder8());
        Paths.Add(pathBuilder.Builder9());

        Paths.Add(pathBuilder.Builder4());
        Paths.Add(pathBuilder.Builder5());
        Paths.Add(pathBuilder.Builder6());

        Paths.Add(pathBuilder.Builder1());//15
        Paths.Add(pathBuilder.Builder2());
        Paths.Add(pathBuilder.Builder3());

        Arrow = Resources.Load<GameObject>("Arrow");

        PlayerPrefs.SetInt("UserID",12);
        /**
        SummaryFile = Application.dataPath + "/Resources/Records/" + "User" + PlayerPrefs.GetInt("UserID") + ".txt";
        PlayerPrefs.SetString("SummaryFile", SummaryFile);
        if (!File.Exists(SummaryFile))
        {
            File.WriteAllText("SummaryFile", "TaskID,Method,StartTime,EndTime,TimeSpan,Waypoint,Collision \n"); 
        }
        **/
    }

    void FixedUpdate()
    {

        //if (Round < 3)
        //{
        //ArrowLine(Paths[Round]);
        // AirPlane(Paths[Round]);
        // }

        
        if (Round == 0)
        {
            ArrowLine(Paths[0]);
        }
        else if (Round == 1)
        {
            ArrowLine(Paths[3]);
        }
        else if (Round == 2)
        {
            ArrowLine(Paths[6]);
        }
        else if (Round == 3)
        {
            ArrowLine(Paths[9]);
        }
        else if (Round == 4)
        {
            ArrowLine(Paths[12]);
        }
        else if (Round == 5)
        {
            ArrowLine(Paths[15]);
        }//second method
        else if (Round == 6)
        {
            AirPlane(Paths[1]);
        }
        else if (Round == 7)
        {
            AirPlane(Paths[4]);
        }
        else if (Round == 8)
        {
            AirPlane(Paths[7]);
        }
        else if (Round == 9)
        {
            AirPlane(Paths[10]);
        }
        if (Round == 10)
        {
            AirPlane(Paths[13]);
        }
        else if (Round == 11)
        {
            AirPlane(Paths[16]);
        }//third method
        else if (Round == 12)
        {
            HeadUpNav(Paths[2]);
        }
        else if (Round == 13)
        {
            HeadUpNav(Paths[5]);
        }
        else if (Round == 14)
        {
            HeadUpNav(Paths[8]);
        }
        else if (Round == 15)
        {
            HeadUpNav(Paths[11]);
        }
        else if (Round == 16)
        {
            HeadUpNav(Paths[14]);
        }
        else if (Round == 17)
        {
            HeadUpNav(Paths[17]);
        }
        else {
            //UnityEditor.EditorApplication.isPlaying = false;
            //Application.Quit();
        }
    }


    void Initialize()
    {
        //if there are arrows, destroy them
        GameObject[] Arrows = GameObject.FindGameObjectsWithTag("Arrows");
        foreach (GameObject a in Arrows)
        { Destroy(a); }


        GameObject[] WayPoints = GameObject.FindGameObjectsWithTag("WayPoint");
        foreach (GameObject w in WayPoints)
        {
            w.GetComponent<Renderer>().enabled = true;}

        // Player default position
        Player.transform.position = new Vector3(0.77f, 1f, 1.33f);
        // Plane default position
        Plane.transform.position = new Vector3(1f, 1f, 2.5f);
        Plane.SetActive(false);
        HeadUp.SetActive(false);
        NextRound.SetActive(false);
        Diamond.SetActive(true);
        
        // If start to build navigation
        BuildArrowLine = true; // apply for any kind of naviagation

        
        /**
        // Record end time
        PlayerPrefs.SetString("EndTime", System.DateTime.Now.ToString());
        File.AppendAllText(PlayerPrefs.GetString("FilePath"), "EndTime: " +PlayerPrefs.GetString("EndTime") + " \n");
        //Datetime startT = System.Convert.ToDateTime(PlayerPrefs.GetString("StartTime"));
        File.AppendAllText(PlayerPrefs.GetString("FilePath"), "Timespan: " +
            (System.Convert.ToDateTime(PlayerPrefs.GetString("EndTime")) - System.Convert.ToDateTime(PlayerPrefs.GetString("StartTime"))).ToString() + " \n");

        File.AppendAllText(PlayerPrefs.GetString("SummaryFile"),TaskID.ToString()+", "+
            PlayerPrefs.GetString("Method")+", "+ PlayerPrefs.GetString("StartTime") + ", " + PlayerPrefs.GetString("EndTime") + ", "+
            (System.Convert.ToDateTime(PlayerPrefs.GetString("EndTime")) - System.Convert.ToDateTime(PlayerPrefs.GetString("StartTime"))).ToString()+ ", " +
            PlayerPrefs.GetInt("Points").ToString()+ ", " + PlayerPrefs.GetInt("Collision").ToString() + "\n");

        PlayerPrefs.SetInt("Points", 0);
        PlayerPrefs.SetInt("Collision", 0);
        WaypointIndex = 0;
        **/

        // Task ID
        TaskID++;
    }

    void ArrowLine(List<Vector3> pathPoints)
    {
        
        Vector3 instantiatePosition;

        // end 1 meter
        float distance1 = Vector3.Distance(Player.transform.position, pathPoints[pathPoints.Count - 1]);
        if (distance1 < 1f)
        {
            DisplayNext = true;
            
            if (DisplayNext)
            {
                Diamond.SetActive(false);
                NextRound.SetActive(DisplayNext);

                if (SteamVR_Actions.default_GrabPinch.GetStateDown(SteamVR_Input_Sources.Any) || SteamVR_Actions.default_GrabGrip.GetStateDown(SteamVR_Input_Sources.Any))
                {
                    DisplayNext = false;
                }
            }
            if (!DisplayNext)
            { 
                Destroy(ArrowInstance, 0.01f);
                Initialize();
                Round += 1;
                BuildArrowLine = true;
                return;
            }
        }


        if (BuildArrowLine)
        {
            PlayerPrefs.SetString("Method", "ArrowLine");

            float D = CalculateDistance(pathPoints);
            // reset vector3 list for arrows
            for (int i = 0; i < pathPoints.Count - 1; i++)
            {
                pathPoints[i] += new Vector3(0f, 0.03f, 0f);

            }

            /**
            PlayerPrefs.SetString("StartTime", System.DateTime.Now.ToString());
            FilePath = Application.dataPath + "/Resources/Records/" + "ArrowLine"+TaskID + ".txt";
            PlayerPrefs.SetString("FilePath", FilePath);
            
            if (!File.Exists(FilePath))
            {
                File.WriteAllText(FilePath, "TaskID: " + TaskID.ToString() + " \n" + "Method:ArrowLine" + "\n" + "StartTime: "
                    + PlayerPrefs.GetString("SetString") + " \n" + "Distance: " + D + "\n");
            }
            **/
            BuildArrowLine = false;

            //set destination
            Diamond.transform.position = pathPoints[pathPoints.Count - 1] + new Vector3(0f, 2f, 0f);
      

            for (int i = 0; i < pathPoints.Count - 1; i++)
            {

                int amount = Mathf.RoundToInt(Mathf.Floor(Vector3.Distance(pathPoints[i + 1], pathPoints[i]) / 0.7f));
                float distance = 1 / amount;
                instantiatePosition = new Vector3(0, 0, 0);
                float x = (pathPoints[i + 1].x - pathPoints[i].x) / amount;
                float y = (pathPoints[i + 1].y - pathPoints[i].y) / amount;
                float z = (pathPoints[i + 1].z - pathPoints[i].z) / amount;

                for (int j = 0; j < amount; j++)
                {
                    instantiatePosition += new Vector3(x, y, z);

                    ArrowInstance = Instantiate(Arrow, pathPoints[i] + instantiatePosition, transform.rotation);
                    ArrowInstance.gameObject.tag = "Arrows";
                    ArrowInstance.transform.LookAt(pathPoints[i + 1]);
                    ArrowInstance.transform.Rotate(90f, -90f, 0f, Space.Self);
                }
            }

        }

    }

    


    void AirPlane(List<Vector3> pathPoints)
    {

        if (BuildArrowLine)
        { //orignal: yes
            PlayerPrefs.SetString("Method", "Plane");

            float D = CalculateDistance(pathPoints);
            Plane.SetActive(true);
            Path = new List<Vector3>();
            //WaypointIndex = 0;
            for (int i = 0; i < pathPoints.Count - 1; i++)
            {
                //Path.Add( pathPoints[i] + new Vector3(0f, 1.5f, 0f));
                pathPoints[i] += new Vector3(0f, 1.5f, 0f);
            }

            /**
            // new file
            PlayerPrefs.SetString("StartTime", System.DateTime.Now.ToString());
            FilePath = Application.dataPath + "/Resources/Records/" + "Plane"+TaskID + ".txt";
            PlayerPrefs.SetString("FilePath", FilePath);
            if (!File.Exists(FilePath))
            {
                File.WriteAllText(FilePath, "TaskID: " + TaskID.ToString() + " \n" + "Method:AirPlane" + "\n" + PlayerPrefs.GetString("StartTime") + " \n" + "Distance: " + D + "\n");
            }
            **/

            BuildArrowLine = false;
            Diamond.transform.position = pathPoints[pathPoints.Count - 1] + new Vector3(0f, 2f, 0f);
        }

        if (WaypointIndex < pathPoints.Count)
        {
            Plane.transform.position = Vector3.MoveTowards(Plane.transform.position, pathPoints[WaypointIndex], PlaneMoveSpeed * Time.deltaTime);
            Vector3 tem = new Vector3(0f, 100f, 0f); //direction the plane is facing
            tem += pathPoints[WaypointIndex];
            Plane.transform.LookAt(tem);
            if (Plane.transform.position == pathPoints[WaypointIndex])
            {
                if (Vector3.Distance(Player.transform.position, Plane.transform.position - new Vector3(0F, 1.5F, 0F)) < 3f) // the distance between user and urning point
                {
                    WaypointIndex += 1;
                }
            }
        }
        // end 
        if (Vector3.Distance(Player.transform.position, pathPoints[pathPoints.Count - 1]) < 1f)
        {
            DisplayNext = true;
            if (DisplayNext)
            {
                Diamond.SetActive(false);
                NextRound.SetActive(true);
                if (SteamVR_Actions.default_GrabPinch.GetStateDown(SteamVR_Input_Sources.Any) || SteamVR_Actions.default_GrabGrip.GetStateDown(SteamVR_Input_Sources.Any))
                {
                    DisplayNext = false;
                }
            }
            if (!DisplayNext)
            {
            
            Plane.SetActive(false);
            Initialize();
            Round += 1;
            return;
            }
        }
    }


    void HeadUpNav(List<Vector3> pathPoints)
    {
        if (BuildArrowLine)
        {
            PlayerPrefs.SetString("Method", "HeadUp");
            float D = CalculateDistance(pathPoints);
            HeadUp.SetActive(true);
            Diamond.transform.position = pathPoints[pathPoints.Count - 1] + new Vector3(0f, 2f, 0f);

            /**
            // new file
            PlayerPrefs.SetString("StartTime", System.DateTime.Now.ToString());
            FilePath = Application.dataPath + "/Resources/Records/" + "HeadUp"+TaskID + ".txt";
            PlayerPrefs.SetString("FilePath", FilePath);
            if (!File.Exists(FilePath))
            {
                File.WriteAllText(FilePath, "TaskID: " + TaskID.ToString() + " \n" + "Method:HeadUp" + "\n" + PlayerPrefs.GetString("StartTime") + " \n" + "Distance: " + D + "\n");
            }
             **/
            BuildArrowLine = false;
        }
            //position
            Vector3 Direction = new Vector3(Camera.main.transform.forward.x *1.5f,
                                          0.5f, Camera.main.transform.forward.z *1.5f);
        HeadUp.transform.position = Player.transform.position + Direction;

        //rotation
        HeadUp.transform.LookAt(pathPoints[Mathf.Min(WaypointIndex, pathPoints.Count - 1)]+ Direction);
        HeadUp.transform.eulerAngles += new Vector3(-90, 270, 0);

        if (Vector3.Distance(Player.transform.position, pathPoints[Mathf.Min(WaypointIndex, pathPoints.Count - 1)]) < 2f)
        {
            WaypointIndex += 1;
        }

        //end
        if (Vector3.Distance(Player.transform.position, pathPoints[pathPoints.Count - 1]) < 1f)
        {
            DisplayNext = true;
            if (DisplayNext)
            {
                Diamond.SetActive(false);
                NextRound.SetActive(true);
                if (SteamVR_Actions.default_GrabPinch.GetStateDown(SteamVR_Input_Sources.Any) || SteamVR_Actions.default_GrabGrip.GetStateDown(SteamVR_Input_Sources.Any))
                {
                    DisplayNext = false;
                }
            }
            if (!DisplayNext)
            {
                HeadUp.SetActive(false);
                Initialize();
                Round += 1;
                return;
            }
            
        }

    }

    float CalculateDistance(List<Vector3> Path)
    {
        float totalD = 0;
        for (int i = 0; i < Path.Count - 1; i++)
            totalD += Vector3.Distance(Path[i], Path[i+1]);
        return totalD;
    }
}
