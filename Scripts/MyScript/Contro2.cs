using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Valve.VR;
using System.IO;

public class Contro2 : MonoBehaviour
{
    //Path
    List<List<Vector3>> Paths;
    List<Vector3> PathForPlane = new List<Vector3>();
    VLB2 pathBuilder;
    //Common 
    public GameObject Player;
    public GameObject FootHold;
    public GameObject Diamond;//sign of destination
    public GameObject NextRound;
    bool DisplayNext = false;
    GameObject Cross;

    public GameObject Arrow; // single arrow load from prefab
    public GameObject ArrowInstance; // arrow line
    public bool BuildNavigation = true; //control build once //BuildArrowLine

    public GameObject Plane;
    float PlaneMoveSpeed = 2.5f;
    int WaypointIndex = 0;

    public GameObject HeadUp;
    public float Distance = 1f;

    public string FilePath;
    public string SummaryFile;
    public int TaskID = 0; // the first one is 1, will be added in initial method.

    public int Round = 16;

    //way points
    public GameObject WP1;
    public GameObject WP2;
    public GameObject WP3;
    public GameObject WP4;
    public GameObject WP5;
    public GameObject WP6;
    public GameObject WP7;
    public GameObject WP8;
    public GameObject WP9;
    public GameObject WP10;
    public GameObject WP11;
    public GameObject WP12;
    public GameObject WP13;
    public GameObject WP14;
    public GameObject WP15;
    public GameObject WP16;
    public GameObject WP17;
    public GameObject WP18;
    public GameObject WP19;
    public GameObject WP20;
    public GameObject WP21;

    //Obstacles 
    public GameObject OB1;
    public GameObject OB2;
    public GameObject OB3;
    public GameObject OB4;
    public GameObject OB5;
    public GameObject OB6;
    public GameObject OB7;
    public GameObject OB8;
    public GameObject OB9;
    public GameObject OB10;
    public GameObject OB11;
    public GameObject OB12;
    public GameObject OB13;
    public GameObject OB14;
    public GameObject OB15;
    public GameObject OB16;
    public GameObject OB17;
    public GameObject OB18;
    public GameObject OB19;
    public GameObject OB20;
    public GameObject OB21;

    void Start()
    {
        PlayerPrefs.SetInt("UserID", 12);

        Arrow = Resources.Load<GameObject>("Arrow");

        pathBuilder = new VLB2();
        Paths = pathBuilder.GetPaths();

        SummaryFile = Application.dataPath + "/Resources/Records/" + "User" + PlayerPrefs.GetInt("UserID") + ".txt";
        PlayerPrefs.SetString("SummaryFile", SummaryFile);
        if (!File.Exists(SummaryFile))
        {
            File.WriteAllText("SummaryFile", "TaskID,Method,StartTime,EndTime,TimeSpan,Waypoint,Collision \n");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(Round)
        {
            // AirPlane  ArrowLine  HeadUpNav
            case 0: AirPlane(Paths[0]); break;
            case 1: AirPlane(Paths[1]); break;
            case 2: AirPlane(Paths[2]); break;
            case 3: AirPlane(Paths[3]); break;
            case 4: AirPlane(Paths[4]); break;
            case 5: AirPlane(Paths[5]); break;
            case 6: AirPlane(Paths[6]); break;
            case 7: AirPlane(Paths[7]); break;

            case 8: HeadUpNav(Paths[0]); break;
            case 9: HeadUpNav(Paths[8]); break;
            case 10: HeadUpNav(Paths[9]); break;
            case 11: HeadUpNav(Paths[10]); break;
            case 12: HeadUpNav(Paths[11]); break;
            case 13: HeadUpNav(Paths[12]); break;
            case 14: HeadUpNav(Paths[13]); break;
            case 15: HeadUpNav(Paths[14]); break;

            case 16: ArrowLine(Paths[0]); break;
            case 17: ArrowLine(Paths[15]); break;
            case 18: ArrowLine(Paths[16]); break;
            case 19: ArrowLine(Paths[17]); break;
            case 20: ArrowLine(Paths[18]); break;
            case 21: ArrowLine(Paths[19]); break;
            case 22: ArrowLine(Paths[20]); break;
            case 23: ArrowLine(Paths[21]); break;

            case 24: Application.Quit(); break;
        }

    }


    void Initialize(List<Vector3> Path)
    {
        PlayerPrefs.SetInt("Points", 0);
        PlayerPrefs.SetInt("Collision", 0);
        float D = CalculateDistance(Path);
        PlayerPrefs.SetString("StartTime", System.DateTime.Now.ToString());
        FilePath = Application.dataPath + "/Resources/Records/" + PlayerPrefs.GetInt("UserID").ToString() + "-" + Round + ".txt";
        PlayerPrefs.SetString("FilePath", FilePath);
        if (!File.Exists(FilePath))
        {
            //Directory.CreateDirectory(Application.dataPath + "/Resources/Records/" + PlayerPrefs.GetInt("UserID").ToString() + "/" + TaskID + ".txt");
            File.WriteAllText(FilePath, "TaskID: " + Round.ToString() + " \n" + "StartTime: " + PlayerPrefs.GetString("StartTime")
                + PlayerPrefs.GetString("SetString") + " \n" + "Distance: " + D +" \n");
        }

        WaypointIndex = 0; // for headUp and plane
        PathForPlane = new List<Vector3>(Path);

        //set all things invisible
        GameObject[] Arrows = GameObject.FindGameObjectsWithTag("Arrows");
        foreach (GameObject a in Arrows)
        { Destroy(a); }
        Plane.SetActive(false);
        HeadUp.SetActive(false);
        NextRound.SetActive(false);
        DisplayNext = false;

        //invisit all waypoints
        GameObject[] WayPointSets = GameObject.FindGameObjectsWithTag("WayPointSet");
        foreach (GameObject WayPointSet in  WayPointSets)
        {
            WayPointSet.SetActive(false);
        }

        //invisit all obstacles
        GameObject[] ObstacleSets = GameObject.FindGameObjectsWithTag("ObstacleSet");
        foreach (GameObject ObstacleSet in ObstacleSets)
        {
            ObstacleSet.SetActive(false);
        }

        //user at starting point, diamond at destination
        Diamond.transform.position = Path.Last();
        Diamond.SetActive(true);
        Player.transform.position = Path[0];
        Plane.transform.position = Path[0];

        ShowWayPoint(Round);
        ShowObstacle(Round);

    }

    void ShowWayPoint(int ID)
    {
        switch (ID)
        {
            case 1: WP1.SetActive(true); break;
            case 2: WP2.SetActive(true); break;
            case 3: WP3.SetActive(true); break;
            case 4: WP4.SetActive(true); break;
            case 5: WP5.SetActive(true); break;
            case 6: WP6.SetActive(true); break;
            case 7: WP7.SetActive(true); break;

            case 9: WP8.SetActive(true); break;
            case 10: WP9.SetActive(true); break;
            case 11: WP10.SetActive(true); break;
            case 12: WP11.SetActive(true); break;
            case 13: WP12.SetActive(true); break;
            case 14: WP13.SetActive(true); break;
            case 15: WP14.SetActive(true); break;

            case 17: WP15.SetActive(true); break;
            case 18: WP16.SetActive(true); break;
            case 19: WP17.SetActive(true); break;
            case 20: WP18.SetActive(true); break;
            case 21: WP19.SetActive(true); break;
            case 22: WP20.SetActive(true); break;
            case 23: WP21.SetActive(true); break;
        }
    }

    void ShowObstacle(int ID)
    {
        switch (ID)
        {
            case 1: OB1.SetActive(true); break;
            case 2: OB2.SetActive(true); break;
            case 3: OB3.SetActive(true); break;
            case 4: OB4.SetActive(true); break;
            case 5: OB5.SetActive(true); break;
            case 6: OB6.SetActive(true); break;
            case 7: OB7.SetActive(true); break;

            case 9: OB8.SetActive(true); break;
            case 10: OB9.SetActive(true); break;
            case 11: OB10.SetActive(true); break;
            case 12: OB11.SetActive(true); break;
            case 13: OB12.SetActive(true); break;
            case 14: OB13.SetActive(true); break;
            case 15: OB14.SetActive(true); break;

            case 17: OB15.SetActive(true); break;
            case 18: OB16.SetActive(true); break;
            case 19: OB17.SetActive(true); break;
            case 20: OB18.SetActive(true); break;
            case 21: OB19.SetActive(true); break;
            case 22: OB20.SetActive(true); break;
            case 23: OB21.SetActive(true); break;
        }
    }

    void ArrowLine(List<Vector3> pathPoints)
    {
        if (BuildNavigation)
        {
            BuildNavigation = false; // check if it will run at least once if i put this sentence here

            PlayerPrefs.SetString("Method", "ArrowLine");

            Initialize(pathPoints);

            File.AppendAllText(PlayerPrefs.GetString("FilePath"), "Method: ArrowLine" + "\n");

            List<Vector3> PathForArrow = new List<Vector3>();
            // reset vector3 list for arrows
            foreach (Vector3 Point in pathPoints)
            { PathForArrow.Add(Point + new Vector3(0f, 0.03f, 0f)); }
            
            //BuildNavigation = false;

            for (int i = 0; i < PathForArrow.Count - 1; i++)
            {
                int amount = Mathf.RoundToInt(Mathf.Floor(Vector3.Distance(PathForArrow[i + 1], PathForArrow[i]) / 0.7f));
                float distance = 1 / amount;
                Vector3 instantiatePosition = new Vector3(0, 0, 0);
                float x = (PathForArrow[i + 1].x - PathForArrow[i].x) / amount;
                float y = (PathForArrow[i + 1].y - PathForArrow[i].y) / amount;
                float z = (PathForArrow[i + 1].z - PathForArrow[i].z) / amount;

                for (int j = 0; j < amount; j++)
                {
                    instantiatePosition += new Vector3(x, y, z);

                    ArrowInstance = Instantiate(Arrow, PathForArrow[i] + instantiatePosition, transform.rotation);
                    ArrowInstance.gameObject.tag = "Arrows";
                    ArrowInstance.transform.LookAt(PathForArrow[i + 1]);
                    ArrowInstance.transform.Rotate(90f, -90f, 0f, Space.Self);
                }
            }
        }

        // end 1 meter
        float distance1 = Vector3.Distance(Player.transform.position, pathPoints[pathPoints.Count - 1]);
        if (distance1 < 1f)
        {
            DisplayNext = true; //get the status of nextRound, for control both NextRound and Diamond

            if (DisplayNext)
            {
                Diamond.SetActive(false);
                NextRound.SetActive(DisplayNext);

                if (SteamVR_Actions.default_GrabPinch.GetStateDown(SteamVR_Input_Sources.Any))
                {
                    Ending();
                    return;
                }
            }
        }

    }

    void HeadUpNav(List<Vector3> pathPoints)
    {
        if (BuildNavigation)
        {
            BuildNavigation = false;

            PlayerPrefs.SetString("Method", "HeadUp");

            Initialize(pathPoints);

            File.AppendAllText(PlayerPrefs.GetString("FilePath"), "Method: HeadUp" + "\n");

            HeadUp.SetActive(true);
 
        }
        //position
        Vector3 Direction = new Vector3(Camera.main.transform.forward.x * 1.5f,
                                      0.3f, Camera.main.transform.forward.z * 1.5f);
        HeadUp.transform.position = Player.transform.position + Direction;

        //rotation
        HeadUp.transform.LookAt(pathPoints[Mathf.Min(WaypointIndex, pathPoints.Count - 1)] + Direction);
        HeadUp.transform.eulerAngles += new Vector3(-90, 270, 0);

        if (Vector3.Distance(Player.transform.position, pathPoints[Mathf.Min(WaypointIndex, pathPoints.Count - 1)]) < 2.3f)
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
                if (SteamVR_Actions.default_GrabPinch.GetStateDown(SteamVR_Input_Sources.Any))
                {
                    Ending();
                    return;
                }
            }
        }

    }

    void AirPlane(List<Vector3> pathPoints)
    {

        if (BuildNavigation)
        { //orignal: yes
            BuildNavigation = false;

            PlayerPrefs.SetString("Method", "Plane");

            Initialize(pathPoints);

            File.AppendAllText(PlayerPrefs.GetString("FilePath"), "Method: Plane" + "\n");

            Plane.SetActive(true);

            for (int i = 0; i < PathForPlane.Count - 1; i++)
            {
                //Path.Add( pathPoints[i] + new Vector3(0f, 1.5f, 0f));
                PathForPlane[i] += new Vector3(0f, 1.5f, 0f);
            }

        }

        if (WaypointIndex < PathForPlane.Count)
        {
            Plane.transform.position = Vector3.MoveTowards(Plane.transform.position, PathForPlane[WaypointIndex], PlaneMoveSpeed * Time.deltaTime);
            Vector3 tem = new Vector3(0f, 100f, 0f); //direction the plane is facing
            tem += PathForPlane[WaypointIndex];
            Plane.transform.LookAt(tem);
            if (Plane.transform.position == PathForPlane[WaypointIndex])
            {
                if (Vector3.Distance(Player.transform.position, Plane.transform.position - new Vector3(0F, 1.5F, 0F)) < 2.3f) // the distance between user and turning point
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
                if (SteamVR_Actions.default_GrabPinch.GetStateDown(SteamVR_Input_Sources.Any))
                {
                    Ending();
                    return;
                }
            }
        }

    }


    float CalculateDistance(List<Vector3> Path)
    {
        float totalD = 0;
        for (int i = 0; i < Path.Count - 1; i++)
            totalD += Vector3.Distance(Path[i], Path[i + 1]);
        return totalD;
    }

    public void Ending()
    {
        PlayerPrefs.SetString("EndTime", System.DateTime.Now.ToString());
        File.AppendAllText(PlayerPrefs.GetString("FilePath"), "EndTime: " + PlayerPrefs.GetString("EndTime") + " \n");
        File.AppendAllText(PlayerPrefs.GetString("FilePath"), "Timespan: " +
            (System.Convert.ToDateTime(PlayerPrefs.GetString("EndTime")) - System.Convert.ToDateTime(PlayerPrefs.GetString("StartTime"))).ToString() + " \n");

        File.AppendAllText(PlayerPrefs.GetString("SummaryFile"), Round.ToString() + ", " +
            PlayerPrefs.GetString("Method") + ", " + PlayerPrefs.GetString("StartTime") + ", " + PlayerPrefs.GetString("EndTime") + ", " +
            (System.Convert.ToDateTime(PlayerPrefs.GetString("EndTime")) - System.Convert.ToDateTime(PlayerPrefs.GetString("StartTime"))).ToString() + ", " +
            PlayerPrefs.GetInt("Points").ToString() + ", " + PlayerPrefs.GetInt("Collision").ToString() + "\n");


        NextRound.SetActive(false);
        DisplayNext = false;
        Round += 1;
        BuildNavigation = true;
    }
}
