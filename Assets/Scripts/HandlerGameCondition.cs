using Zenject;

public enum GameCondition
{
    Game,
    Menu  
}
public class HandlerGameCondition : ManagedBehaviour
{
    [Inject] private BlockCursor _blockCursor;

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
    
    private GameCondition _currenGameCondition = GameCondition.Game;

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
