using Zenject;

public enum GameCondition
{
    Game,
    Menu  
}
public class HandlerGameCondition : ManagedBehaviour
{
    [Inject] private BlockCursor _blockCursor;

    public GameCondition GetGameCondition
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
            _blockCursor.LockCursor.Add(this);
        else
            _blockCursor.LockCursor.Remove(this);
    }
}
