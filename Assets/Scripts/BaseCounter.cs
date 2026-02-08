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
        private PlayerController _сharacterController;

        [Inject]
        public void Construct(PlayerController playerController)
        {
            _сharacterController = playerController;
        }
        private void Awake()
        {
            Text = GetComponent<TMP_Text>();
            _сharacterController.OnHitPlayer += ResetCount;
        }
        protected virtual void OnDestroy() => _сharacterController.OnHitPlayer -= ResetCount;

        protected virtual void ResetCount()
        {
            Count = default;
            Text.text = default;
        }

        protected virtual void CountChange(int count){}
    }
}
