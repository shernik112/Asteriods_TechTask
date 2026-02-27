using Project.Player;
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
        public virtual void Construct(EventBus eventBus)
        {
            EventBus = eventBus;
        }
        
        protected virtual void Awake()
        {
            Text = GetComponent<TMP_Text>();
            EventBus.OnHitPlayer += ResetCount;
        }
        
        protected virtual void OnDestroy() => EventBus.OnHitPlayer -= ResetCount;

        private void ResetCount()
        {
            StopAllCoroutines();
            Count = default;
            Text.text = default;
        }
    }
}
