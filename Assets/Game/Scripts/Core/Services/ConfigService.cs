using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

public class ConfigService : IConfigService
{
    private const string ConfigFolderPath = "Config/";

    public T LoadConfig<T>(string configPath) where T : class
    {
        var fullPath = $"{ConfigFolderPath}{configPath}";
        var textAsset = Resources.Load<TextAsset>(fullPath);

        if (textAsset == null)
        {
            throw new System.IO.FileNotFoundException($"Config file not found at path: {fullPath}");
        }

        return JsonConvert.DeserializeObject<T>(textAsset.text);
    }

    public async UniTask SaveProgressAsync<T>(string savePath, T progress) where T : class
    {
        await UniTask.RunOnThreadPool(() =>
        {
            var json = JsonConvert.SerializeObject(progress, Formatting.Indented);
            PlayerPrefs.SetString(savePath, json);
            PlayerPrefs.Save();
        });
    }

    public async UniTask<T> LoadProgressAsync<T>(string savePath) where T : class
    {
        if (!PlayerPrefs.HasKey(savePath)) return null;

        return await UniTask.RunOnThreadPool(() =>
        {
            var json = PlayerPrefs.GetString(savePath);
            return JsonConvert.DeserializeObject<T>(json);
        });
    }
}
