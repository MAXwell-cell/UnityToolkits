using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class WebGLSaveData
{
     [DllImport("__Internal")]
    private static extern void SaveData(string key, string value);

    [DllImport("__Internal")]
    private static extern string LoadData(string key);

    public void SaveGameData(string key, string value)
    {
        SaveData(key, value);
    }

    public string LoadGameData(string key)
    {
        return LoadData(key);
    }
}
