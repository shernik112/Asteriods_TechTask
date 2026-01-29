using Zenject;

public enum GameCondition
{
    Game,
    Menu  
}
public class HandlerGameCondition : ManagedBehaviour
{
    [Inject] private BlockCursor _blockCursor;
    private GameCondition _currentGameCondition = GameCondition.Game;

    public GameCondition GetGameCondition
    {
        get { return _currentGameCondition; }
        set
        {
            if (_currentGameCondition != value)
            {
                _currentGameCondition = value;
                HandleNewCondition();
            }
            
        }
    }

    private void Start()
    {
        HandleNewCondition();
    }

    private void HandleNewCondition()
    {
        if (_currentGameCondition == GameCondition.Game)
            _blockCursor.LockCursor.Add(this);
        else
            _blockCursor.LockCursor.Remove(this);
    }
}
