using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SomeClass : MonoBehaviour
{

    public List<string> phraseList = new List<string>();

    void OnGUI()
    {
        int i = 0;
        foreach (string str in phraseList)
        {
            if (GUI.Button(new Rect(100, 20 * i, 200, 20), str))
            {
                Debug.Log("Box #" + i + " is working");
            }
            i++;
        }
    }
    void Update()
    {
        if (Input.GetKeyUp("z"))
        {
            phraseList.Add("Test string 1");
            phraseList.Add("Test string 2");
            phraseList.Add("Test string 3");
            foreach (string str in phraseList)
            {
                Debug.Log(str);
            }
        }
    }
}