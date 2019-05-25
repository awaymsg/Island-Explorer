using UnityEngine;

public static class ConvertGameData
{
    public static Party ConvertParty(GameData data)
    {
        int count = data.names.Length;
        if (count != 0)
        {
            Party party = new Party();
            for (int i = 0; i < count; i++)
            {
                ACharacter c;
                if (i == 0)
                {
                    c = new Leader();
                } else
                {
                    c = new Member();
                }
                c.name = data.names[i];
                c.isFemale = data.isFemale[i];
                c.strength = data.strength[i];
                c.defense = data.defense[i];
                c.hunting = data.hunting[i];
                c.scouting = data.scouting[i];
                c.tracking = data.tracking[i];
                c.diplomacy = data.diplomacy[i];
                c.foodconsumption = data.foodconsumption[i];
                party.AddPartyMember(c);
            }
            return party;
        } else
        {
            Debug.LogError("Party is empty!");
            return null;
        }
    }
}
