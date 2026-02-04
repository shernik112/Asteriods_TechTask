using System;
using UnityEngine;

public class HandlerScore : BaseCounter
{
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
    protected override void AddToCount(int countEnemy)
    {
        Count += countEnemy;
        Text.text = Count.ToString();
    }
}
