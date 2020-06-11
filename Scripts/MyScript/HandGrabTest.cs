using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HandGrabTest : MonoBehaviour
{
    bool collided = true;
    GameObject theOther;


    // Update is called once per frame
    void Update()
    {
        if (SteamVR_Actions.default_GrabPinch.GetStateDown(SteamVR_Input_Sources.Any) || SteamVR_Actions.default_GrabGrip.GetStateDown(SteamVR_Input_Sources.Any))
        {
            print("trigger");
            //print(theOther.name);
            
                if (theOther.GetComponent<Renderer>().isVisible)
                {

                    theOther.GetComponent<Renderer>().enabled = false;
                }


            
        }
    }

    void OnTriggerEnter(Collider other)
    {
        print("gt");
        collided = true;
        theOther = other.gameObject;
        
    }

    void OnCollisionExit()
    {
        collided = false;
        theOther = null;
    }

    void OnCollisionEnter(Collision collision)
    {
        print("gtClision");
    }
}
