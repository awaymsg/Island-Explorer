using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewGameMenu : MonoBehaviour
{
    public TMP_InputField islandinput;
    private string islandname;

    public void ContinueButton()
    {
        islandname = islandinput.text;
        Debug.Log(islandname);
    }
}
