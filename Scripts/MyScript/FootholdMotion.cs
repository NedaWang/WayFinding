using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootholdMotion : MonoBehaviour
{
    public GameObject Player;
    float Distance = 0.8f;


    // Update is called once per frame
    void Update()
    {
        Vector3 Direction = new Vector3(Camera.main.transform.forward.x * Distance,
                                          0.05f, Camera.main.transform.forward.z * Distance);
        transform.position = Player.transform.position + Direction;
    }
}
