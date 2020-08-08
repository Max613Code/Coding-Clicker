using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveSystem
{
    public static readonly string SAVE_FOLDER = Path.Combine(Application.persistentDataPath, "Save");

    public static void Init()
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }

    public static void Save(string saveString)
    {
        File.WriteAllText(Path.Combine(SAVE_FOLDER, "save.json"), saveString);
    }

    public static bool SaveFound()
    {
        if (File.Exists(Path.Combine(SAVE_FOLDER, "save.json")))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static string Load()
    {
        if (SaveFound())
        {
            return File.ReadAllText(Path.Combine(SAVE_FOLDER, "save.json"));
        }
        else
        {
            return null;
        }
    }
}
