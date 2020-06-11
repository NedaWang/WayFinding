using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class PathRecorder : MonoBehaviour
{
    GameObject Player;
    void Start()
    {
        Player = GameObject.Find("Player");
        CreateText();
    }

    void CreateText()
    {
        string path = Application.dataPath + "/Resources/Records/"+ System.DateTime.Now.ToString("MMdd") +".txt";
        //Create it if it doesn't exist
        if (!File.Exists(path))
        {
            string time = System.DateTime.Now.ToString("HH-mm-ss");
            File.WriteAllText(path,time + " " + Player.transform.position + "\n");
        }

        //string content = "Login date: " + System.DateTime.Now.ToString("MMdd-HHmmss") + "\n";
        //File.AppendAllText(path, content);

    }
}
