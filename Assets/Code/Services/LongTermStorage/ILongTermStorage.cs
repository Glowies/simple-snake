using System;
using UnityEngine;

public interface ILongTermStorage
{
    float GetFloat(string key);
    int GetInt(string key);
    string GetString(string key);

    void SetFloat(string key, float value);
    void SetInt(string key, int value);
    void SetString(string key, string value);

    void DeleteKey(string key);
    bool HasKey(string key);
    void DeleteAll();
}

