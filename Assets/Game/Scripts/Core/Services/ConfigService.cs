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
            throw new FileNotFoundException($"Config file not found at path: {fullPath}");
        }

        return JsonConvert.DeserializeObject<T>(textAsset.text);
    }

    public async UniTask<T> LoadConfigAsync<T>(string configPath) where T : class
    {
        var fullPath = $"{ConfigFolderPath}{configPath}";
        var resourceRequest = Resources.LoadAsync<TextAsset>(fullPath);

        await resourceRequest.ToUniTask();

        if (resourceRequest.asset == null)
        {
            throw new FileNotFoundException($"Config file not found at path: {fullPath}");
        }

        var textAsset = (TextAsset)resourceRequest.asset;
        return JsonConvert.DeserializeObject<T>(textAsset.text);
    }

    public void SaveConfig<T>(string configPath, T config) where T : class
    {
        // For Resources, we can't save at runtime. This would need addressables or file system
        throw new NotImplementedException("Saving to Resources is not supported at runtime. Use SaveConfigAsync with different storage.");
    }

    public async UniTask SaveConfigAsync<T>(string configPath, T config) where T : class
    {
        // Implementation for saving would go here when we set up proper storage
        await UniTask.CompletedTask;
        throw new NotImplementedException("Saving not yet implemented");
    }
}
