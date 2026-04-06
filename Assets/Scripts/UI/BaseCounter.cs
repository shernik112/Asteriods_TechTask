using Project.System;
using TMPro;
using UnityEngine;
using Zenject;

namespace Project.UI
{
    [RequireComponent(typeof(TMP_Text))]
    public abstract class BaseCounter : MonoBehaviour
    {
        protected int Count;
        protected TMP_Text Text;
        protected EventBus EventBus;

        [Inject]
        public void Construct(EventBus eventBus)
        {
            EventBus = eventBus;
        }
        
        protected virtual void Awake()
        {
            EventBus.OnHitPlayer += ResetCount;
            Text = GetComponent<TMP_Text>();
        }
        
        protected virtual void OnDestroy() => 
            EventBus.OnHitPlayer -= ResetCount;

        private void ResetCount()
        {
            StopAllCoroutines();
            Count = default;
            Text.text = default;
        }
    }
}
