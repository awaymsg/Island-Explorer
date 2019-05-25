using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ACharacter
{
    public bool isFemale;
    public string name;
    public string displayname;
    public int charclass; //new (0-6, 0 is leader)
    public int strength;
    public int defense;
    public int hunting;
    public int scouting;
    public int tracking;
    public int diplomacy;
    public float foodconsumption;

    public void SetDisplayName(string dispname)
    {
        displayname = dispname;
    }
}
