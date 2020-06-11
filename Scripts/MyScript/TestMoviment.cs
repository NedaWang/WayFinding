using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using System.IO;

public class TestMoviment : MonoBehaviour
{
    GameObject Player;
    Transform Transform;
    GameObject Foothold;
    public GameObject Cross;
    float Distance = 0.8f;
    string path;
    bool Forward = true;
    bool isHeld = false;
    float Timer = 1f;
    public SteamVR_Action_Vector2 touchPadAction;
    void Start()
    {
        Player = GameObject.Find("Player");
        Transform = Player.transform;
        Foothold = GameObject.Find("Foothold");
        
        path = Application.dataPath + "/Resources/Records/" + System.DateTime.Now.ToString("MMdd-HHmmss") + ".txt";
        if (!File.Exists(path))
        {
            string time = System.DateTime.Now.ToString("HH-mm-ss");
            File.WriteAllText(path, time + " " + Player.transform.position + "\n");
        }
        
    }

    void Update()
    {
        Vector3 Direction = new Vector3(Camera.main.transform.forward.x * Distance,
                                          0f, Camera.main.transform.forward.z * Distance);
        Vector3 Level1 = Player.transform.position + new Vector3(0F, 0.1F, 0F);
        Vector3 Level2 = Player.transform.position + new Vector3(0F, 0.2F, 0F);

        Forward = !Physics.Raycast(Level1, Direction, Direction.magnitude) // Mathf.Sqrt(Distance * Distance * 2)
                && !Physics.Raycast(Level2, Direction, Direction.magnitude);

        Cross.SetActive(!Forward);
        Vector2 touchpadValue = touchPadAction.GetAxis(SteamVR_Input_Sources.Any);
        if (SteamVR_Actions.default_Teleport.GetStateDown(SteamVR_Input_Sources.Any))
        {
            if (touchpadValue.y > 0)
            {
                if (Forward)
                {
                    transform.position += Direction;
                    string time = System.DateTime.Now.ToString("HH-mm-ss");
                    File.AppendAllText(path, time + " " + Player.transform.position + "\n");
                }
                else
                {
                    File.AppendAllText(path, "Collision \n");
                }

                isHeld = true;
            }
        }

        if (SteamVR_Actions.default_Teleport.GetStateUp(SteamVR_Input_Sources.Any))
        {
            isHeld = false;
        }

        Timer -= Time.deltaTime;
        if (Timer < 0)
        {
            if (isHeld && Forward)
            {
                transform.position += Direction;
                string time = System.DateTime.Now.ToString("HH-mm-ss");
                File.AppendAllText(path, time + " " + Player.transform.position + "\n");
            } else if (isHeld)
            {
                File.AppendAllText(path, "Collision \n");
            }           
            Timer = 0.4f;
        } 

        

        

        
        //Timer -= Time.deltaTime;

        //if (Timer < 0){
        //Debug.Log("!!!");
        //Timer = 3f;
        //}


        //Vector3 Level3 = Player.transform.position + new Vector3(0F, 0.3F, 0F);
        //Vector3 Level4 = Player.transform.position + new Vector3(0F, 0.5F, 0F);
        //Vector3 Level5 = Player.transform.position + new Vector3(0F, 0.8F, 0F);
        /**
        if (!Physics.Raycast(Level1, Direction, Direction.magnitude) // Mathf.Sqrt(Distance * Distance * 2)
                && !Physics.Raycast(Level2, Direction, Direction.magnitude))
        {
            usability = true;
            Cross.SetActive(false);
        }
        else
        {
            usability = false;
            Cross.SetActive(true);
        }
        **/

        //if (SteamVR_Actions.default_Teleport.GetStateDown(SteamVR_Input_Sources.Any))
        //{
        //isHeld = true;
        //transform.Translate(Camera.main.transform.forward * 1f );
        //transform.position += Camera.main.transform.forward * 1f;
        /**
        if (!Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, 0.85f))
        {
            transform.position += Direction;
        }
        **/
        //if (!Physics.Raycast(Transform.position, Camera.main.transform.forward, Mathf.Sqrt(Distance * Distance * 2)))
        //&& !Physics.Raycast(Transform.position, Camera.main.transform.forward, 0.85f)
        //if (!Physics.Raycast(Level1, Direction, Direction.magnitude) // Mathf.Sqrt(Distance * Distance * 2)
        //&& !Physics.Raycast(Level2, Direction, Direction.magnitude))
        //if(usability)
        //{

        // transform.position += Direction;

        //string time = System.DateTime.Now.ToString("HH-mm-ss");
        //File.AppendAllText(path, time + " " + Player.transform.position + "\n");
        // }
        // else {
        //File.AppendAllText(path, "Collision \n");
        // }
        //  }
        //if (SteamVR_Actions.default_Teleport.GetStateUp(SteamVR_Input_Sources.Any))
        // {
        //    isHeld = false;
        //  }


    }

}
