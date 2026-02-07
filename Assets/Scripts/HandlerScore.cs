using UnityEngine;
using Zenject;

public class HandlerScore : BaseCounter
{
    [SerializeField] private int countAsteroid;
    [SerializeField] private int countUfo;
    
    private GetFinalScore _getFinalScore;

    [Inject]
    public void Construct(GetFinalScore getFinalScore)
    {
        _getFinalScore = getFinalScore;
    }
    
    public void CountDefeatedEnemy(int countDefeatedEnemy) => AddToCount(countDefeatedEnemy);

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
