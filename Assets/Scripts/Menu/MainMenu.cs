using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button continuebutton;
    public TextMeshProUGUI continuetext;

    public void Start()
    {
        string path = Application.persistentDataPath + "/gamedata.sav";
        Debug.Log(path);
        if (!File.Exists(path))
        {
            continuebutton.interactable = false;
            continuetext.color = Color.black;
        }
    }

    public void ContinueButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
