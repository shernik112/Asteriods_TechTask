using UnityEngine;
using Zenject;

namespace Project.System
{
    public enum GameCondition
    {
        Game,
        Menu  
    }

    public class HandlerGameCondition : MonoBehaviour
    {
        private GameCondition _currenGameCondition = GameCondition.Game;
        private BlockCursor _blockCursor = new BlockCursor();

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

        private void Start()
        {
            HandleNewCondition();
        }

        private void HandleNewCondition()
        {
            if (_currenGameCondition == GameCondition.Game)
                _blockCursor.SetCursorCondition(true);
            else
                _blockCursor.SetCursorCondition(false);
        }
    }
}