using System.IO;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace Code
{
    public class Fetcher
    {
        public async UniTask<T> Fetch<T>(string url)
        {
            var dir = Application.streamingAssetsPath;
            var fullPath = dir + url;
            var text = await File.ReadAllTextAsync(fullPath);
            await UniTask.Delay(Random.Range(500, 1500));
            return JsonConvert.DeserializeObject<T>(text);
        }
    }
}