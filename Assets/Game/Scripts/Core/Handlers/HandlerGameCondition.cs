
namespace Project.System
{
    public enum GameCondition
    {
        Game,
        Menu  
    }

    public class HandlerGameCondition
    {
        private GameCondition _currentGameCondition = GameCondition.Game;
        private readonly BlockCursor _blockCursor = new BlockCursor();

        public GameCondition GameCondition
        {
            set
            {
                if (_currentGameCondition == value) 
                    return;
                _currentGameCondition = value;
                HandleNewCondition();
            }
        }

        public HandlerGameCondition() =>
            HandleNewCondition();

        private void HandleNewCondition() =>
            _blockCursor.SetCursorCondition(_currentGameCondition == GameCondition.Game);
    }
}