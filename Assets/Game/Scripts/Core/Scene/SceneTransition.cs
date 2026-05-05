using System.Collections;
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
        private WaitForSeconds _waitHiding;

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
            _waitHiding = new WaitForSeconds(_showRestartUI.ShowTime);
            _transitionIn = true;
        }

        private void OnDestroy()
        {
            _playerController.DeathHandler.OnHitPlayer -= StartTransition;
            _restartButton.OnRestartGame -= StartTransition;
        }

        private void StartTransition()
        {
            StopAllCoroutines();
            StartCoroutine(Transition());
        }
        
        private IEnumerator Transition()
        {
            _transitionIn = !_transitionIn;
            
            if (_transitionIn)
            {
                _showRestartUI.SmoothShow();
                yield return _waitHiding;
                StartCoroutine(_transitionIn ? FadeIn() : FadeOut());
            }
            else
            {
                yield return StartCoroutine(_transitionIn ? FadeIn() : FadeOut());
                _showRestartUI.SmoothShow();
            }
        }

        private IEnumerator FadeOut()
        {
            while (_mask.alphaCutoff < 1f)
            {
                _mask.alphaCutoff += Time.deltaTime * speed;
                yield return null;
            }
            _mask.alphaCutoff = 1f;
        }

        private IEnumerator FadeIn()
        {
            _mask.alphaCutoff = 1f;
            while (_mask.alphaCutoff > 0f)
            {
                _mask.alphaCutoff -= Time.deltaTime * speed;
                yield return null;
            }
            _mask.alphaCutoff = 0f;
        }
    }
}
