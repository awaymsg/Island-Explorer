using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ChooseCharacterScreen : MonoBehaviour
{
    private Party party;
    private GameData data;
    private ACharacter leader1;
    private ACharacter leader2;
    private ACharacter leader3;
    private int randomseed;
    public TextMeshProUGUI charname1, charname2, charname3;
    public TextMeshProUGUI charstats1, charstats2, charstats3;
    public TMP_InputField islandinput;
    private string islandname;

    void Start()
    {
        party = new Party();
        islandname = islandinput.text;
        randomseed = Random.Range(-10000, 10000);
        leader1 = CharacterGenerator.GenerateCharacter(true, 0, 0);
        leader2 = CharacterGenerator.GenerateCharacter(true, 0, 0);
        leader3 = CharacterGenerator.GenerateCharacter(true, 0, 0);
        SetText();
    }

    private void SetText()
    {
        charname1.text = leader1.name;
        charname2.text = leader2.name;
        charname3.text = leader3.name;
        charstats1.text = "Strength: " + leader1.strength + "\n" +
                        "Defense: " + leader1.defense + "\n" +
                        "Hunting: " + leader1.hunting + "\n" +
                        "Scouting: " + leader1.scouting + "\n" +
                        "Tracking: " + leader1.tracking + "\n" +
                        "Diplomacy: " + leader1.diplomacy;
        charstats2.text = "Strength: " + leader2.strength + "\n" +
                        "Defense: " + leader2.defense + "\n" +
                        "Hunting: " + leader2.hunting + "\n" +
                        "Scouting: " + leader2.scouting + "\n" +
                        "Tracking: " + leader2.tracking + "\n" +
                        "Diplomacy: " + leader2.diplomacy;
        charstats3.text = "Strength: " + leader3.strength + "\n" +
                        "Defense: " + leader3.defense + "\n" +
                        "Hunting: " + leader3.hunting + "\n" +
                        "Scouting: " + leader3.scouting + "\n" +
                        "Tracking: " + leader3.tracking + "\n" +
                        "Diplomacy: " + leader3.diplomacy;
    }

    public void StartGame1()
    {
        party.AddPartyMember(leader1);
        data = new GameData(party, null, randomseed, islandname);
        SaveSystem.SaveGame(data);
        SceneManager.LoadScene("GameScene");
    }

    public void StartGame2()
    {
        party.AddPartyMember(leader2);
        data = new GameData(party, null, randomseed, islandname);
        SaveSystem.SaveGame(data);
        SceneManager.LoadScene("GameScene");
    }

    public void StartGame3()
    {
        party.AddPartyMember(leader3);
        data = new GameData(party, null, randomseed, islandname);
        SaveSystem.SaveGame(data);
        SceneManager.LoadScene("GameScene");
    }

}
