using System;
using UnityEngine;
using Zenject;

public class HandlerScore : BaseCounter
{
    [Inject] private GetFinalScore _getFinalScore;
    [SerializeField] private int countAsteroid;
    [SerializeField] private int countUfo;
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

    protected override void ResetCount()
    {
        _getFinalScore.ShowFinalScore(Count);
        base.ResetCount();
    }

    protected override void AddToCount(int countEnemy)
    {
        Count += countEnemy;
        Text.text = Count.ToString();
    }
}
