using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public interface IConfigService
{
    T LoadConfig<T>(string configPath) where T : class;
    UniTask<T> LoadConfigAsync<T>(string configPath) where T : class;
    void SaveConfig<T>(string configPath, T config) where T : class;
    UniTask SaveConfigAsync<T>(string configPath, T config) where T : class;
}
