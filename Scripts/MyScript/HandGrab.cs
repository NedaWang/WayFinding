using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using System.IO;
using Valve.VR.InteractionSystem;

public class HandGrab : MonoBehaviour
{
    bool collided;
    GameObject theOther;
    Transform parentTransform;
    int point = 0;

    void Start()
    {
    }

    void Update()
    {
       
        if (SteamVR_Actions.default_GrabGrip.GetStateDown(SteamVR_Input_Sources.Any))
        {
            if (theOther)
            {
                if (theOther.GetComponent<Renderer>().isVisible)
                {
                    int Point = PlayerPrefs.GetInt("Points") + 1;
                    PlayerPrefs.SetInt("Points",PlayerPrefs.GetInt("Points")+1);
                    File.AppendAllText(PlayerPrefs.GetString("FilePath"), "WayPont: " + PlayerPrefs.GetInt("Points") + " " + System.DateTime.Now.ToString() + "\n");
                    theOther.GetComponent<Renderer>().enabled = false;
                }
                
                
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("WayPoint"))
        {
            collided = true;
            theOther = other.gameObject;
        }
    }

    void OnCollisionExit()
    {
        collided = false;
        theOther = null;
    }
}
