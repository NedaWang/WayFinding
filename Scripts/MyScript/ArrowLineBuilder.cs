using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowLineBuilder : MonoBehaviour
{
    List<Vector3> pathPoints;
    VectorListBuilder pathBuilder;

    int amount;
    float distance;
    //float lerpValue = 0;
    Vector3 instantiatePosition;
    Vector3 gap;

    public GameObject Arrow;
    public GameObject ArrowInstance;

    public GameObject destination;

    GameObject Player;

    private void Awake()
    {
        pathBuilder = new VectorListBuilder();
    }

    void Start()
    {
        Player = GameObject.Find("Player");
        destination = GameObject.Find("Diamond");
        
        Arrow = Resources.Load<GameObject>("Arrow");

        //Arrow = Instantiate(Resources.Load("Prefabs/Arrow", typeof(GameObject))) as GameObject;

        pathPoints = pathBuilder.Builder1();
        destination.transform.position = pathPoints[pathPoints.Count - 1] + new Vector3(0f,2f,0f);
        //destination.transform.localScale -= new Vector3(0.5f, 0.5f, 0.5f);


        for (int i = 0; i < pathPoints.Count - 1; i++)
        {
            pathPoints[i] += new Vector3(0f, 0.1f, 0f);
        }

        for (int i = 0; i < pathPoints.Count - 1; i++)
        {

            amount=Mathf.RoundToInt(Mathf.Floor(Vector3.Distance(pathPoints[i + 1], pathPoints[i]) / 0.7f));
            // pathPoints[i]: origin
            // pathPoints[i+1]: destination
            //amount = Mathf.RoundToInt(Vector3.Distance(pathPoints[i + 1], pathPoints[i]) / 0.4f);
            distance = 1 / amount;
            instantiatePosition = new Vector3(0, 0, 0);
            float x = (pathPoints[i + 1].x - pathPoints[i].x) / amount;
            float y = (pathPoints[i + 1].y - pathPoints[i].y) / amount;
            float z = (pathPoints[i + 1].z - pathPoints[i].z) / amount;
            for (int j = 0; j < amount; j++)
            {
                //We increase our lerpValue
                //lerpValue += distance;
                //Get the position
                //instantiatePosition = Vector3.Lerp(pathPoints[i], pathPoints[i + 1], lerpValue);
                //Instantiate the object
                instantiatePosition += new Vector3(x,y,z);

                ArrowInstance = Instantiate(Arrow, pathPoints[i]+instantiatePosition, transform.rotation);
                ArrowInstance.transform.LookAt(pathPoints[i+1]);
                ArrowInstance.transform.Rotate(90f, -90f, 0f, Space.Self);
            }

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
