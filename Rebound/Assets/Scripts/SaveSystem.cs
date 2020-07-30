/* Primary Author: Trent Lewis
 */

using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveSystem
{
    public static void SavedCurrentLevel (GameManager gm)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/level.num";
        FileStream stream = new FileStream(path, FileMode.Create);

        SavedLevel level = new SavedLevel(gm);

        formatter.Serialize(stream, level);
        stream.Close();
    }

    public static SavedLevel LoadCurrentLevel()
    {
        string path = Application.persistentDataPath + "/level.num";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SavedLevel level = formatter.Deserialize(stream) as SavedLevel;
            stream.Close();

            return level;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}