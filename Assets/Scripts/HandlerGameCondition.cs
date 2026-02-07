using Zenject;

public enum GameCondition
{
    Game,
    Menu  
}

public class HandlerGameCondition : ManagedBehaviour
{
    private GameCondition _currenGameCondition = GameCondition.Game;
    private BlockCursor _blockCursor;

    public GameCondition GameCondition
    {
        get { return _currenGameCondition; }
        set
        {
            if (_currenGameCondition != value)
            {
                _currenGameCondition = value;
                HandleNewCondition();
            }
            
        }
    }
    
    [Inject]
    public void Construct(BlockCursor blockCursor)
    {
        _blockCursor = blockCursor;
    }

    private void Start()
    {
        HandleNewCondition();
    }

    private void HandleNewCondition()
    {
        if (_currenGameCondition == GameCondition.Game)
        {
            PauseAll.Remove(this);
            _blockCursor.LockCursor.Add(this);
        }
        else
        {
            PauseAll.Add(this);
            _blockCursor.LockCursor.Remove(this);
        }
    }
}
