using Project.System;
using UnityEngine;

namespace Project
{
    [CreateAssetMenu(fileName = "EnemiesSpawnerData", menuName = "Scriptable Objects/EnemiesSpawnerData")]
    public class EnemiesControllerData : ScriptableObject
    {
        [field: SerializeField] public GameObject FragmentAsteroidPrefab { get; private set; }
        [field: SerializeField] public GameObject AsteroidPrefab { get; private set; }
        [field: SerializeField] public GameObject UfoPrefab { get; private set; }
        
        [field: SerializeField] public int StartCountAsteroids {get; private set; }
        [field: SerializeField] public FloatRange RangeTimeAsteroid {get; private set; }
        [field: SerializeField] public FloatRange RangeTimeUfo {get; private set; }
        
        [field:SerializeField]  public float DefaultPosOffset { get;private set; }
        [field:SerializeField]  public float RotateOffset { get; private set; }
        
        [field:SerializeField]  public float CreateFragmentRotate { get; private set; }
        [field:SerializeField]  public float LowerFragmentRotate { get; private set; }

    }
}