using Project.Player;

namespace Project.System
{
    public class PauseHandler : ManagedBehaviour
    {
        private PlayerController _playerController;
        private RestartInvoke _restartInvoke;

        public void Construct(PlayerController playerController, RestartInvoke restartInvoke)
        {
            _playerController = playerController;
            _restartInvoke = restartInvoke;
        }

        private void OnEnable()
        {
            _playerController.OnHitPlayer += SetPause;
        }

        private void OnDisable()
        {
            _playerController.OnHitPlayer -= SetPause;
        }

        private void SetPause() => PauseAll.Add(this);

        protected override bool UpdateWhenPause => true;
    
    }
}
