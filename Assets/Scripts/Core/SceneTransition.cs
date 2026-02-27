using System.Collections;
using Project.UI;
using UnityEngine;
using Zenject;

namespace Project.System
{
    public class SceneTransition : MonoBehaviour
    {


        private readonly float _speed = 3f;

        private bool _transitionIn;
        private SpriteMask _mask;
        private ShowUI _showUI;
        private EventBus _eventBus;
        private WaitForSeconds _waitHiding;

        [Inject]
        public void Construct(
            EventBus eventBus)
        {
            _eventBus = eventBus;
        }
    
        private void Awake()
        {
            _eventBus.OnHitPlayer += StartTransition;
            _eventBus.OnRestartGame += StartTransition;
            _mask = GetComponentInChildren<SpriteMask>();
            _showUI = GetComponentInChildren<ShowUI>();
            _waitHiding = new WaitForSeconds(_showUI.ShowTime);
            _transitionIn = true;
        }

        private void OnDestroy()
        {
            _eventBus.OnHitPlayer -= StartTransition;
            _eventBus.OnRestartGame -= StartTransition;
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
                _showUI.SmoothShow();
                yield return _waitHiding;
                StartCoroutine(_transitionIn ? FadeIn() : FadeOut());
            }
            else
            {
                yield return StartCoroutine(_transitionIn ? FadeIn() : FadeOut());
                _showUI.SmoothShow();
            }
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
