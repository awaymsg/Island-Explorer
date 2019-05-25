using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Party
{
    public string name;
    private List<ACharacter> members = new List<ACharacter>();
    private int strength;
    private int defense;
    private int hunting;
    private int scouting;
    private int tracking;
    private int diplomacy;
    private float foodconsumption;

    public void UpdatePartyStats()
    {
        strength = 0;
        defense = 0;
        hunting = 0;
        scouting = 0;
        tracking = 0;
        diplomacy = 0;
        foodconsumption = 0;
        foreach (ACharacter c in members)
        {
            strength += c.strength;
            defense += c.defense;
            hunting += c.hunting;
            scouting += c.scouting;
            tracking += c.tracking;
            diplomacy += c.diplomacy;
            foodconsumption += c.foodconsumption;
        }
    }

    public int GetMemberCount()
    {
        return members.Count;
    }

    public ACharacter GetMember(int index)
    {
        return members[index];
    }

    public string PartyNames()
    {
        string str = "";
        foreach (ACharacter c in members)
        {
            str += c.name + " \n";
        }
        return str;
    }

    public void AddPartyMember(ACharacter c)
    {
        members.Add(c);
        UpdatePartyStats();
    }

    public void RemovePartyMember(ACharacter c)
    {
        members.Remove(c);
        UpdatePartyStats();
    }

    public int GetStrength()
    {
        return strength;
    }

    public int GetDefense()
    {
        return defense;
    }

    public int GetHunting()
    {
        return hunting;
    }

    public int GetScouting()
    {
        return scouting;
    }

    public int GetTracking()
    {
        return tracking;
    }

    public int GetDiplomacy()
    {
        return diplomacy;
    }

    public float GetFoodConsumption()
    {
        return foodconsumption;
    }
}
