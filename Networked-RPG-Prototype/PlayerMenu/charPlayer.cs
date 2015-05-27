using UnityEngine;
using System.Collections;
using System;

public class charPlayer : IComparable<charPlayer>{
    
    public string name;
    public string race; 

    //Class to set properties for each new character 
    public charPlayer(string newName, string newRace)
    {
        name = newName;
        race = newRace;
    }

    public int CompareTo(charPlayer other)
    {
        if(other == null)
        {
            return 1; 
        }
        else
        {
            return 0;
        }
    }
    
}
