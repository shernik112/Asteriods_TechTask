using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Project.System
{
    public class AssetLoader : IAssetLoader, IDisposable
    {
        private readonly List<AsyncOperationHandle> _handles = new();
        
        public async UniTask<GameObject> LoadAssetAsync(AssetReferenceGameObject assetReference,
            CancellationToken cancellationToken)
        {
            if (assetReference == null)
                throw new NullReferenceException("Asset reference is null");
            
            var handle = Addressables.LoadAssetAsync<GameObject>(assetReference);
            var prefab = await handle.ToUniTask(cancellationToken: cancellationToken);

            _handles.Add(handle);
            return prefab;
        }

        public void Dispose() 
            => ReleaseAll();
        
        public void ReleaseAll()
        {
            foreach (var handle in _handles)
            {
                if (handle.IsValid())
                    Addressables.Release(handle);
            }
            
            _handles.Clear();
        }
    }
}
