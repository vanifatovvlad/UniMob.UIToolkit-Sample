using System;
using System.IO;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code
{
    public class Fetcher
    {
        public async UniTask<T> Fetch<T>(string url)
        {
            await UniTask.Delay(Random.Range(500, 1500));

            if (Random.value > 0.75f)
            {
                throw new InvalidOperationException($"Network error");
            }

            try
            {
                var dir = Application.streamingAssetsPath;
                var fullPath = dir + url;
                var text = await File.ReadAllTextAsync(fullPath);

                return JsonConvert.DeserializeObject<T>(text);
            }
            catch
            {
                throw new InvalidOperationException($"Resource '{url}' not exist");
            }
        }
    }
}