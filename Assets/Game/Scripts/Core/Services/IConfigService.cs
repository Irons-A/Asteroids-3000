using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Scripts.Core.Sevices
{
    public interface IConfigService
    {
        T LoadConfig<T>(string configPath) where T : class;
        UniTask SaveProgressAsync<T>(string savePath, T progress) where T : class;
        UniTask<T> LoadProgressAsync<T>(string savePath) where T : class;
    }
}
