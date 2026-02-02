using System;
using TMPro;
using UnityEngine;

public class HandlerScore : ManagedBehaviour
{
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
            AddToCount(countAsteroid);
        else if(typeof(UFOBehaviour) == enemyType)
            AddToCount(countUfo);
    }

    private void AddToCount(int countEnemy)
    {
        _count = countEnemy;
        _text.text = _count.ToString();
    }
}
