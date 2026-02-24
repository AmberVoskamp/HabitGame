using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class Config
{
    public bool tutorialFinished;
    public int currentSpikeDificulty;
    public List<LevelData> levelsData;

    private static string SaveFilenName()
    {
        string saveFile = $"{Application.persistentDataPath}/save.text";
        return saveFile;
    }

    public static void Save(Config config)
    {
        string saveFile = SaveFilenName();
        Debug.Log($"{saveFile}");
        if (!File.Exists(saveFile))
        {
            File.CreateText(saveFile);
        }

        File.WriteAllText(saveFile, JsonUtility.ToJson(config, true));
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
        return config;
    }
}
