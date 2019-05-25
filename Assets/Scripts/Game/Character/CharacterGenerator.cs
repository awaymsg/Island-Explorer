using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharacterGenerator
{
    private static readonly string[] maleNames =
        { "David", "Balith", "Adam", "Shops", "Arty", "Artison", "Miroslav",
        "Martin", "Atkinson", "Connor", "Maximilion", "Hammer", "Tim", "Bicep",
        "Krond", "Wayne", "Bruce", "Samuel", "Tyrone", "Saul", "Ace",
        "Raoul", "Kenneth", "Thomas", "Bastion", "Bradley", "Zachary", "Zim",
        "Bruno", "Fox", "Thelonius", "Maurice", "Eagle", "Amadeus", "Wolf" };

    private static readonly string[] femaleNames =
        { "Amy", "Arlinn", "Elizabeth", "Ella", "Brienne", "Thalia", "Gloria",
        "Amelia", "Sally", "Dolphin", "Rachel", "Sommer", "Kathryn", "Camelia",
        "Delphine", "Daphne", "Katia", "Svetlana", "Kseniaa", "Dana", "Wanda",
        "Merle", "Juliet", "Jennifer", "Sila", "Samantha", "Tanya", "Hayley",
        "Mercedes", "Kayla", "Xena", "Anya", "Linda", "April", "Kronda" };

    private static readonly string[] lastNames =
        { "Johnson", "Mulder", "Scully", "de Tanserville", "von Arc", "Anderson", "de la Cruz",
        "van der Potato", "Atkinson", "Shieldbearer", "the Wise", "of Lindervale", "Smithson", "Atchoo",
        "Chang", "Radish", "Williams", "the Strong", "Hauser", "Mutato", "Morris",
        "Patel", "McDonalds", "Kane", "Raza", "Siri", "Ramazan", "DaVern",
        "Voltaire", "Chandler", "Lone-Wolf", "Grarg", "Grond", "Vupelon", "del Rico" };

    private static readonly string[] maleTitles =
        { "Sir", "Lord", "Admiral", "Captain", "Doctor", "Count", "Master", "Baron", "Duke" };

    private static readonly string[] femaleTitles =
        { "Madam", "Lady", "Admiral", "Captain", "Doctor", "Countess", "Mistress", "Baroness", "Duchess" };

    private static readonly int maxStatTotal = 40;

    private static readonly int minStatTotal = 30;

    private static readonly int maxStatTotalLeader = 60;

    private static readonly int minStatTotalLeader = 55;

    public static ACharacter GenerateCharacter(bool isLeader, int charClass, int proficiency)
    {
        ACharacter character;
        if (isLeader)
        {
            character = new Leader();
            int titleval = Random.Range(0, 9);
            float genderRoll = Random.value;
            if (genderRoll <= 0.5)
            {
                character.isFemale = true;
            }
            if (character.isFemale)
            {
                character.name = femaleTitles[titleval] + " ";
            } else
            {
                character.name = maleTitles[titleval] + " ";
            }
            character.charclass = 0;
        } else
        {
            character = new Member();
            float genderRoll = Random.value;
            if (genderRoll <= 0.5)
            {
                character.isFemale = true;
            }
            character.charclass = charClass;
        }
        int firstnameidx = Random.Range(0, 35);
        int lastnameidx = Random.Range(0, 35);
        if (character.isFemale)
        {
            character.name += femaleNames[firstnameidx];
        } else
        {
            character.name += maleNames[firstnameidx];
        }

        character.name += " " + lastNames[lastnameidx];

        character = GenerateStats(character, proficiency);

        return character;
    }

    private static ACharacter GenerateStats(ACharacter c, int proficiency)
    {
        int strength = 0, defense = 0, hunting = 0, scouting = 0, tracking = 0, diplomacy = 0;
        if (c.charclass == 0)
        {
            int total = 0;
            while (total < minStatTotalLeader || total > maxStatTotalLeader)
            {
                strength = Random.Range(2, 16);
                defense = Random.Range(2, 16);
                hunting = Random.Range(2, 16);
                scouting = Random.Range(2, 16);
                tracking = Random.Range(2, 16);
                diplomacy = Random.Range(2, 16);
                total = strength + defense + hunting + scouting + tracking + diplomacy;
            }
            c.strength = strength;
            c.defense = defense;
            c.hunting = hunting;
            c.scouting = scouting;
            c.tracking = tracking;
            c.diplomacy = diplomacy;
        } else
        {

        }

        return c;
    }
}