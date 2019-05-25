using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    
    public static void SaveGame(GameData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/gamedata.sav";

        try
        {
            FileStream stream = new FileStream(path, FileMode.Create);

            formatter.Serialize(stream, data);
            stream.Close();
        } catch (FileNotFoundException e)
        {
            Debug.LogError(e);
        }
    }

    public static GameData LoadGame()
    {
        string path = Application.persistentDataPath + "/gamedata.sav";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            try
            {
                FileStream stream = new FileStream(path, FileMode.Open);

                GameData data = formatter.Deserialize(stream) as GameData;

                stream.Close();
                return data;
            } catch (FileNotFoundException e)
            {
                Debug.LogError(e);
                return null;
            }
        } else
        {
            Debug.LogError("File does not exist");
            return null;
        }
    }
}
