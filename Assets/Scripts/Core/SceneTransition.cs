using System.Collections;
using Project.Player;
using UnityEngine;
using Zenject;

namespace Project.System
{
    public class SceneTransition : MonoBehaviour
    {
        private readonly float _speed = 2.5f;

        private bool _transitionIn;
        private SpriteMask _mask;
        private PlayerController _playerController;
        private RestartButton _restartButton;

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
            _playerController.OnHitPlayer += Transition;
            _restartButton.OnRestartGame += Transition;
            _mask = GetComponentInChildren<SpriteMask>();
            _transitionIn = true;
        }

        private void OnDestroy()
        {
            _playerController.OnHitPlayer -= Transition;
            _restartButton.OnRestartGame -= Transition;
        }

        private void Transition()
        {
            StopAllCoroutines();
            _transitionIn = !_transitionIn;
            
            StartCoroutine(_transitionIn ? FadeIn() : FadeOut());
        }

        private IEnumerator FadeOut()
        {
            while (_mask.alphaCutoff < 1f)
            {
                _mask.alphaCutoff += Time.deltaTime * _speed;
                yield return null;
            }
            _mask.alphaCutoff = 1f;
        }

        private IEnumerator FadeIn()
        {
            _mask.alphaCutoff = 1f;
            while (_mask.alphaCutoff > 0f)
            {
                _mask.alphaCutoff -= Time.deltaTime * _speed;
                yield return null;
            }
            _mask.alphaCutoff = 0f;
        }
    }
}
