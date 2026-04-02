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

    // Link to the JSLib plugin
    [DllImport("__Internal")]
    private static extern void DownloadFile(string filename, string content);

    public bool tutorialFinished;
    public bool finishedAllBosses;
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

#if UNITY_WEBGL && !UNITY_EDITOR
        SyncFiles();
        //PlayerPrefs.Save();
#endif

        return config;
    }

    public static void Download(Config config)
    {
        string json = JsonUtility.ToJson(config, true);
        string filename = "MyGameSave.json";

#if UNITY_WEBGL && !UNITY_EDITOR
            DownloadFile(filename, json);
#else
        // Fallback for Editor: Just print to console or save to Desktop
        Debug.Log("Download triggered. Content: " + json);
#endif
    }
}
