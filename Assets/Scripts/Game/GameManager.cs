using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI infotext;
    public TextMeshProUGUI islandtext;
    public TerrainGenerator terraingen;
    public TerrainGrid terraingrid;
    public Camera camerer;
    [SerializeField]
    private int randomSeed;
    private Party party = new Party();
    private GameData data;
    private GameObject currenttile;
    private Vector3 newcamerapos;
    private Vector3 newplayerpos;

    // Start is called before the first frame update
    void Start()
    {
        data = SaveSystem.LoadGame();
        islandtext.text = data.islandname;
        Random.seed = data.randomseed;
        terraingen.Generate();
        party = ConvertGameData.ConvertParty(data);
        infotext.text = "Expedition Members: \n" + party.PartyNames();

        terraingrid = new TerrainGrid(terraingen.GetTileArray(), terraingen.GetXSize(), terraingen.GetZSize());
        currenttile = terraingrid.GetCurrentTile();

        SetCameraPosition();
        SetPlayerPosition();
    }

    void SetPlayerPosition()
    {
        newplayerpos = currenttile.transform.position;
        newplayerpos.y += 1f;
        player.transform.position = newplayerpos;
    }

    void SetCameraPosition()
    {
        newcamerapos = currenttile.transform.position;
        newcamerapos.y += 6f;
        camerer.transform.position = newcamerapos;
        camerer.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
    }

    void MovePlayer(int direction)
    {
        currenttile = terraingrid.GetNextTile(direction);
        Debug.Log(currenttile.transform.position);
    }

    void UpdatePos()
    {
        newcamerapos = currenttile.transform.position;
        newplayerpos = newcamerapos;
        newcamerapos.y += 6f;
        newplayerpos.y += 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            MovePlayer(0);
            UpdatePos();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            MovePlayer(1);
            UpdatePos();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            MovePlayer(2);
            UpdatePos();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            MovePlayer(3);
            UpdatePos();
        }
        camerer.transform.position = Vector3.Lerp(camerer.transform.position, newcamerapos, Time.deltaTime * 2f);
        player.transform.position = Vector3.Lerp(player.transform.position, newplayerpos, Time.deltaTime * 10f);
    }
}