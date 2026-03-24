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
        protected PlayerController PlayerController;

        [Inject]
        public void Construct(PlayerController playerController)
        {
            PlayerController = playerController;
        }
        
        protected virtual void Awake()
        {
            PlayerController.View.OnHitPlayer += ResetCount;
            Text = GetComponent<TMP_Text>();
        }
        
        protected virtual void OnDestroy() => 
            PlayerController.View.OnHitPlayer -= ResetCount;

        private void ResetCount()
        {
            StopAllCoroutines();
            Count = default;
            Text.text = default;
        }
    }
}
