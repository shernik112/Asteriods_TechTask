using Project.Player;
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
        private PlayerController _playerController;

        [Inject]
        public virtual void Construct(PlayerController playerController)
        {
            _playerController = playerController;
        }
        
        protected virtual void Awake()
        {
            Text = GetComponent<TMP_Text>();
            _playerController.OnHitPlayer += ResetCount;
        }
        
        protected virtual void OnDestroy() => _playerController.OnHitPlayer -= ResetCount;

        protected virtual void ResetCount()
        {
            StopAllCoroutines();
            Count = default;
            Text.text = default;
        }
    }
}
