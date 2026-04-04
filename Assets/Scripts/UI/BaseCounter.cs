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
        protected HandlerScore HandlerScore;

        [Inject]
        public void Construct(
            PlayerController playerController, 
            HandlerScore handlerScore)
        {
            PlayerController = playerController;
            HandlerScore = handlerScore;
        }
        
        protected virtual void Awake()
        {
            PlayerController.OnHitPlayer += ResetCount;
            Text = GetComponent<TMP_Text>();
        }
        
        protected virtual void OnDestroy() => 
            PlayerController.OnHitPlayer -= ResetCount;

        private void ResetCount()
        {
            StopAllCoroutines();
            Count = default;
            Text.text = default;
        }
    }
}
