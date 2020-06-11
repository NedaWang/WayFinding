using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Manager : MonoBehaviour
{
    List<Vector3> pathPoints;
    VectorListBuilder pathBuilder;

    GameObject Player;

    public GameObject destination;

    public GameObject Arrow; // single arrow load from prefab
    public GameObject ArrowInstance; // arrow line

    public GameObject HeadUp;
    public GameObject FootHold;
    public float Distance = 0.8f;


    public GameObject Plane;
    float PlaneMoveSpeed = 2.5f;
    int WaypointIndex = 0;
    List<Vector3> PlanePathPoint;

    private void Awake()
    {
        //PlayerPrefs.SetString("LastTaskID", currentId);
        //PlayerPrefs.GetString("Last")
        pathBuilder = new VectorListBuilder();
    }

    void Start()
    {
        Player = GameObject.Find("Player");

        destination = GameObject.Find("Diamond");
        //create a path
        pathPoints = pathBuilder.Builder3();
        //put diamond at destination
        destination.transform.position = pathPoints[pathPoints.Count - 1] + new Vector3(0f, 2f, 0f);
        //load single arrow prefab
        Arrow = Resources.Load<GameObject>("Arrow");
        //find plane
        //Plane = GameObject.Find("Plane");

        //create arrow line [method 1]
        ArrowLineStart();

        //Plane [method 2]
        //AirPlaneStart();

        //HeadUp [method3]
        //HeadUpStart();
    }

    // Update is called once per frame
    void Update()
    {
        //game finish when user arrive destination
        if (Vector3.Distance(Player.transform.position, pathPoints[pathPoints.Count - 1]) < 1f)
        {
            UnityEditor.EditorApplication.isPlaying = false;
            //Application.Quit();
        }

        //Plane [method 2]
        //AirPlaneUpdate();

        //HeadUp [method 3]
        //HeadUpUpdate();
    }


    void ArrowLineStart()
    {
        for (int i = 0; i < pathPoints.Count - 1; i++)
        {
            pathPoints[i] += new Vector3(0f, 0.03f, 0f);
        }
        for (int i = 0; i < pathPoints.Count - 1; i++)
        {

            int amount = Mathf.RoundToInt(Mathf.Floor(Vector3.Distance(pathPoints[i + 1], pathPoints[i]) / 0.7f));
            float distance = 1 / amount;
            Vector3 instantiatePosition = new Vector3(0, 0, 0);
            float x = (pathPoints[i + 1].x - pathPoints[i].x) / amount;
            float y = (pathPoints[i + 1].y - pathPoints[i].y) / amount;
            float z = (pathPoints[i + 1].z - pathPoints[i].z) / amount;
            for (int j = 0; j < amount; j++)
            {
                instantiatePosition += new Vector3(x, y, z);

                ArrowInstance = Instantiate(Arrow, pathPoints[i] + instantiatePosition, transform.rotation);
                ArrowInstance.transform.LookAt(pathPoints[i + 1]);
                ArrowInstance.transform.Rotate(90f, -90f, 0f, Space.Self);
            }
        }
    }

    void AirPlaneStart()
    {
        Plane.SetActive(true);
        PlanePathPoint = new List<Vector3>();
        foreach (Vector3 location in pathPoints)
        {
            Vector3 newLocation = location + new Vector3(0.0f, 1.5f, 0.0f);
            PlanePathPoint.Add(newLocation);
        }
    }

    void AirPlaneUpdate()
    {
        if (WaypointIndex < PlanePathPoint.Count)
        {
            Plane.transform.position = Vector3.MoveTowards(Plane.transform.position, PlanePathPoint[WaypointIndex], PlaneMoveSpeed * Time.deltaTime);
            Vector3 tem = new Vector3(0f, 100f, 0f); //direction the plane is facing
            tem += PlanePathPoint[WaypointIndex];
            Plane.transform.LookAt(tem);
            if (Plane.transform.position == PlanePathPoint[WaypointIndex])
            {
                if (Vector3.Distance(Player.transform.position, Plane.transform.position-new Vector3(0F,1.5F,0F))<1f)
                {
                    WaypointIndex += 1;
                }                
            }
        }
    }

    void HeadUpStart()
    {
        HeadUp.SetActive(true);
        FootHold.SetActive(false);
    }

    void HeadUpUpdate()
    {
        //position
        Vector3 Direction = new Vector3(Camera.main.transform.forward.x * Distance,
                                          0.1f, Camera.main.transform.forward.z * Distance);
        HeadUp.transform.position = Player.transform.position + Direction;

        //rotation
        HeadUp.transform.LookAt(pathPoints[Mathf.Min(WaypointIndex, pathPoints.Count-1)]);
        HeadUp.transform.eulerAngles += new Vector3(-90, 270, 0);

        if (Vector3.Distance(Player.transform.position, pathPoints[Mathf.Min(WaypointIndex, pathPoints.Count - 1)]) < 1.5f)
        {
            WaypointIndex += 1;
        }
    }
}
