using System;
using UnityEngine;

namespace Project.System
{
    public interface IPoolable
    {
        event Action<GameObject> OnDeactivation;
        void ReturnToPool();
    }
}