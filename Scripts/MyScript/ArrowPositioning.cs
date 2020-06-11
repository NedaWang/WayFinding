using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ArrowPositioning : MonoBehaviour
{
    List<Vector3> pathPoints;
    VectorListBuilder listBuilder;
    int PlaneWaypointIndex = 0;

    float Distance = 0.8f;

    GameObject Player;
    public GameObject destination;


    // Start is called before the first frame update
    void Start()
    {
        listBuilder = new VectorListBuilder();
        pathPoints = listBuilder.Builder1();
        Player = GameObject.Find("Player");
        destination = GameObject.Find("Diamond");
        destination.transform.position = pathPoints[pathPoints.Count - 1] + new Vector3(0f, 2f, 0f);
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(Player.transform.position, pathPoints[pathPoints.Count - 1]) < 1f)
        {
            //UnityEditor.EditorApplication.isPlaying = false;
            //Application.Quit();
            SceneManager.LoadScene(sceneName: "Plane1");
        }
        //position
        Vector3 Direction = new Vector3(Camera.main.transform.forward.x * Distance,
                                          0.1f, Camera.main.transform.forward.z * Distance);
        gameObject.transform.position = Player.transform.position + Direction;

        //gameObject.transform.LookAt(pathPoints[PlaneWaypointIndex]);
        if (Vector3.Distance(Player.transform.position, pathPoints[PlaneWaypointIndex])<1.5f)
        {
            PlaneWaypointIndex += 1;
        }
       /**
        Vector3 look = pathPoints[PlaneWaypointIndex+1] - pathPoints[PlaneWaypointIndex];
        Quaternion rotation = Quaternion.LookRotation(Direction);
        gameObject.transform.rotation = rotation;
    **/
        gameObject.transform.LookAt(pathPoints[PlaneWaypointIndex]);
        gameObject.transform.eulerAngles += new Vector3(-90, 270, 0);

        
    }
}
