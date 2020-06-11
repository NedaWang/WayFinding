using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{

    List<Vector3> pathPoints;
    string Method;

    //arrowLine
    public GameObject Arrow = Resources.Load<GameObject>("Arrow");
    public GameObject ArrowInstance; 

    public Task(List<Vector3> pathPoints, string Method)
    {
        this.pathPoints = pathPoints;
        this.Method = Method;
    }

    public void Start()
    {
        if (Method == "ArrowLine")
        {
            Debug.Log("ArrowLine" + pathPoints.Count.ToString());
            ArrowLineStart();
        }
        else if (Method == "Plane")
        {
            Debug.Log("Plane" + pathPoints.Count.ToString());
        }
        else if (Method == "HeadUp")
        {
            Debug.Log("HeadUp"+ pathPoints.Count.ToString());
        }
    }

    
    void Update()
    {
        
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
                GameObject Arrow = Resources.Load<GameObject>("Arrow");
                ArrowInstance = Instantiate(Arrow, pathPoints[i] + instantiatePosition, transform.rotation);
                ArrowInstance.transform.LookAt(pathPoints[i + 1]);
                ArrowInstance.transform.Rotate(90f, -90f, 0f, Space.Self);
            }
        }
    }
}
