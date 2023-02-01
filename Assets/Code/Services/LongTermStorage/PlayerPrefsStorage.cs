using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsStorage : ILongTermStorage
{
    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    public void DeleteKey(string key)
    {
        PlayerPrefs.DeleteKey(key);
        PlayerPrefs.Save();
    }

    public bool GetBool(string key)
    {
        var value = GetInt(key);
        return value > 0;
    }

    public float GetFloat(string key) => PlayerPrefs.GetFloat(key);

    public int GetInt(string key) => PlayerPrefs.GetInt(key);

    public string GetString(string key) => PlayerPrefs.GetString(key);

    public bool HasKey(string key) => PlayerPrefs.HasKey(key);

    public void SetBool(string key, bool value)
    {
        SetInt(key, value ? 1 : 0);
    }

    public void SetFloat(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
        PlayerPrefs.Save();
    }

    public void SetInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save();
    }

    public void SetString(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
        PlayerPrefs.Save();
    }
}