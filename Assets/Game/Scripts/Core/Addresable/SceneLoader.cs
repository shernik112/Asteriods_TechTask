using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Project.System
{
    public class SceneLoader : ISceneLoader
    {
        public async UniTask<SceneInstance> LoadSceneAsync(AssetReference reference, LoadSceneMode mode, CancellationToken ct)
        {
            if (reference == null)
                throw new NullReferenceException();
            
            AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(reference, mode);
            
            return await handle.ToUniTask(cancellationToken: ct);
        }
    }
}
