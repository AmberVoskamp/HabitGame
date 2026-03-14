using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.InteropServices;

/// <summary>
/// This is the script where all the players data is stored, saved and read
/// </summary>

[Serializable]
public class Config
{
    [DllImport("__Internal")]
    private static extern void SyncFiles();

    public bool tutorialFinished;
    public int currentSpikeDificulty;
    public List<LevelData> levelsData;

    private static string SaveFilenName()
    {
        string saveFile = Path.Combine(Application.persistentDataPath, "save.json");
        return saveFile;
    }

    public static void Save(Config config)
    {
        string saveFile = SaveFilenName();

      /*  if (!Directory.Exists(saveFile))
        {
            Directory.CreateDirectory(saveFile);
        }
*/
        string json = JsonUtility.ToJson(config, true);
        File.WriteAllText(saveFile, json);

#if UNITY_WEBGL && !UNITY_EDITOR
        SyncFiles();
        //PlayerPrefs.Save();
#endif
    }

    public static Config Load()
    {
        string saveFile = SaveFilenName();
        if (!File.Exists(saveFile))
        {
            Save(new Config());
        }
        string jsonText = File.ReadAllText(saveFile);
        Config config = JsonUtility.FromJson<Config>(jsonText);

        Debug.Log($"JSON CONFIG \n {jsonText}");

        return config;
    }
}
