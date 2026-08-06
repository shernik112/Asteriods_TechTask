using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Project.System
{
     public interface IAssetLoader
     {
         public UniTask<T> LoadAsset<T>(AssetReferenceGameObject assetReference)
             where T : Object;
     }
    public class AssetLoader : IAssetLoader
    {
        
    }
}
