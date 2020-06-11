using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaneMovement : MonoBehaviour
{
    GameObject Diamond;
    private float moveSpeed = 1.5f;

    private int waypointIndex = 0;

    List<Vector3> pathPoints;
    List<Vector3> pathPoints2;
    VectorListBuilder listBuilder;
    GameObject Player;


    [SerializeField]
    public GameObject guide;

    private void Start()
    {
        Diamond = GameObject.Find("Diamond");
        Player = GameObject.Find("Player");
        listBuilder = new VectorListBuilder();
        pathPoints = listBuilder.Builder1();
        pathPoints2 = new List<Vector3>();
        foreach (Vector3 location in pathPoints)
        {
            Vector3 newLocation = location + new Vector3(0.0f, 1.5f, 0.0f);
            //Debug.Log(newLocation);
            pathPoints2.Add(newLocation);

        }
        Diamond.transform.position = pathPoints[pathPoints.Count - 1] + new Vector3(0f, 2f, 0f);
    }

    private void Update()
    {
        if (Vector3.Distance(Player.transform.position, pathPoints[pathPoints.Count - 1]) < 1f)
        {
            Debug.Log("11");
            //UnityEditor.EditorApplication.isPlaying = false;
            //Application.Quit();
            SceneManager.LoadScene(sceneName: "Line1");
        }

        if (waypointIndex < pathPoints2.Count)
        {
            //Debug.Log(gameObject.transform.position + " " + pathPoints[waypointIndex]);
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, pathPoints2[waypointIndex], moveSpeed * Time.deltaTime);
            //gameObject.transform.rotation = Quaternion.LookRotation(pathPoints2[waypointIndex]);
            Vector3 tem = new Vector3(0f, 100f, 0f);
            tem += pathPoints2[waypointIndex];
            gameObject.transform.LookAt(tem);
            if (gameObject.transform.position == pathPoints2[waypointIndex])
            {
                //Debug.Log("111");
                waypointIndex += 1;
            }
            //Debug.Log(waypointIndex);
        }
    }


}
