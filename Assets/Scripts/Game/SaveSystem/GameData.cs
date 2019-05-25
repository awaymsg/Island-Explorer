using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [System.Serializable]
public class GameData
{
    public string[] names;
    public bool[] isFemale;
    public int[] strength;
    public int[] defense;
    public int[] hunting;
    public int[] scouting;
    public int[] tracking;
    public int[] diplomacy;
    public float[] foodconsumption;
    public int randomseed;
    public string islandname;

    //public bool[,] explored;
    //public float[,] foodamt;

    public GameData(Party party, GameObject[,] tilearray, int randomseed, string islandname)
    {
        this.randomseed = randomseed;
        this.islandname = islandname;
        if (tilearray != null) {

        }
        int count = party.GetMemberCount();
        names = new string[count];
        isFemale = new bool[count];
        strength = new int[count];
        defense = new int[count];
        hunting = new int[count];
        scouting = new int[count];
        tracking = new int[count];
        diplomacy = new int[count];
        foodconsumption = new float[count];

        for (int i = 0; i < count; i++)
        {
            ACharacter c = party.GetMember(i);
            names[i] = c.name;
            isFemale[i] = c.isFemale;
            strength[i] = c.strength;
            defense[i] = c.defense;
            hunting[i] = c.hunting;
            scouting[i] = c.scouting;
            tracking[i] = c.tracking;
            diplomacy[i] = c.diplomacy;
            foodconsumption[i] = c.foodconsumption;
        }
    }
}
