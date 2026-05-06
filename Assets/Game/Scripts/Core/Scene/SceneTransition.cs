using Cysharp.Threading.Tasks;
using System.Threading;
using Project.Player;
using Project.UI;
using UnityEngine;
using Zenject;

namespace Project.System
{
    public class SceneTransition : MonoBehaviour
    {
        [SerializeField] private float speed;

        private bool _transitionIn;
        private SpriteMask _mask;
        private ShowRestartUI _showRestartUI;
        private PlayerController _playerController;
        private RestartButton _restartButton;

        private CancellationTokenSource _transitionCts;
        
        [Inject]
        public void Construct(
            PlayerController playerController,
            RestartButton restartButton)
        {
            _playerController = playerController;
            _restartButton = restartButton;
        }
    
        private void Awake()
        {
            _playerController.DeathHandler.OnHitPlayer += StartTransition;
            _restartButton.OnRestartGame += StartTransition;
            _mask = GetComponentInChildren<SpriteMask>();
            _showRestartUI = GetComponentInChildren<ShowRestartUI>();
            _transitionIn = true;
        }

        private void OnDestroy()
        {
            _playerController.DeathHandler.OnHitPlayer -= StartTransition;
            _restartButton.OnRestartGame -= StartTransition;
            
            _transitionCts?.Cancel();
            _transitionCts?.Dispose();
        }

        private void StartTransition()
        {
            _transitionCts?.Cancel();
            _transitionCts?.Dispose();
            _transitionCts = new CancellationTokenSource();
            
            Transition(_transitionCts.Token).Forget();
        }
        
        private async UniTask Transition(CancellationToken cts)
        {
            _transitionIn = !_transitionIn;
            
            if (_transitionIn)
            {
                _showRestartUI.SmoothShow();
                await UniTask.Delay(_showRestartUI.ShowTimeMs, cancellationToken: cts);
                await FadeIn(cts);
            }
            else
            {
                await FadeOut(cts);
                _showRestartUI.SmoothShow();
            }
        }

        private async UniTask FadeOut(CancellationToken cts)
        {
            while (_mask.alphaCutoff < 1f)
            {
                _mask.alphaCutoff += Time.deltaTime * speed;
                await UniTask.NextFrame(cts);
            }
            _mask.alphaCutoff = 1f;
        }

        private async UniTask FadeIn(CancellationToken cts)
        {
            _mask.alphaCutoff = 1f;
            while (_mask.alphaCutoff > 0f)
            {
                _mask.alphaCutoff -= Time.deltaTime * speed; 
                await UniTask.NextFrame(cts);
            }
            _mask.alphaCutoff = 0f;
        }
    }
}
