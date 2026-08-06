using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Project.System
{
    public interface ISceneLoader
    {
        UniTask<SceneInstance> LoadSceneAsync(
            AssetReference reference, 
            LoadSceneMode mode, 
            CancellationToken ct);
    }
}
