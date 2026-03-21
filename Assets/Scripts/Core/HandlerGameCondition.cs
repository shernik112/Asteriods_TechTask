
namespace Project.System
{
    public enum GameCondition
    {
        Game,
        Menu  
    }

    public class HandlerGameCondition
    {
        private GameCondition _currenGameCondition = GameCondition.Game;
        private readonly BlockCursor _blockCursor = new BlockCursor();

        public GameCondition GameCondition
        {
            set
            {
                if (_currenGameCondition == value) 
                    return;
                _currenGameCondition = value;
                HandleNewCondition();
            }
        }

        public HandlerGameCondition() =>
            HandleNewCondition();

        private void HandleNewCondition() =>
            _blockCursor.SetCursorCondition(_currenGameCondition == GameCondition.Game);
    }
}