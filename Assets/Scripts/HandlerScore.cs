using System;
using TMPro;
using UnityEngine;
using Zenject;

public class HandlerScore : ManagedBehaviour
{
    [Inject] private CharacterController _chController;
    [SerializeField] private int countAsteroid;
    [SerializeField] private int countUfo;
    private int _count;
    private TMP_Text _text;
    public override void ManagedInintialize()
    {
        _text = GetComponent<TMP_Text>();
    }

    public void CountDefeatedEnemy( Type enemyType)
    {
        if (typeof(AsteroidBehaviour) == enemyType)
        {
            Debug.Log($"{typeof(HandlerScore)} GetAsteroidScore");
            AddToCount(countAsteroid);
        }
        else if (typeof(UFOBehaviour) == enemyType)
        {
            Debug.Log($"{typeof(HandlerScore)} GetAsteroidScore");
            AddToCount(countAsteroid);
        }
    }

    private void OnEnable() => _chController.OnHitPlayer += ResetCount;
    private void OnDisable() => _chController.OnHitPlayer -= ResetCount;

    private void ResetCount()
    {
        _count = default;
        _text.text = default;
    }
    private void AddToCount(int countEnemy)
    {
        _count += countEnemy;
        _text.text = _count.ToString();
    }
}
