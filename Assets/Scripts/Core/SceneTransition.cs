using System.Collections;
using Project.UI;
using UnityEngine;
using Zenject;

namespace Project.System
{
    public class SceneTransition : MonoBehaviour
    {
        private readonly float _speed = 2.5f;

        private bool _transitionIn;
        private SpriteMask _mask;
        private ShowUI _showUI;
        private EventBus _eventBus;

        [Inject]
        public void Construct(
            EventBus eventBus)
        {
            _eventBus = eventBus;
        }
    
        private void Awake()
        {
            _eventBus.OnHitPlayer += Transition;
            _eventBus.OnRestartGame += Transition;
            _mask = GetComponentInChildren<SpriteMask>();
            // _showUI = GetComponentInChildren<ShowUI>();
            _transitionIn = true;
        }

        private void OnDestroy()
        {
            _eventBus.OnHitPlayer -= Transition;
            _eventBus.OnRestartGame -= Transition;
        }

        private void Transition()
        {
            StopAllCoroutines();
            _transitionIn = !_transitionIn;
            
            StartCoroutine(_transitionIn ? FadeIn() : FadeOut());
            // _showUI.SmoothShow();
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
