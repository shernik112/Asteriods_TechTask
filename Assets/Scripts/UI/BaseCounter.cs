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
        protected PlayerController PlayerController;

        [Inject]
        public virtual void Construct(PlayerController playerController)
        {
            PlayerController = playerController;
        }
        
        protected virtual void Awake()
        {
            Text = GetComponent<TMP_Text>();
            PlayerController.OnHitPlayer += ResetCount;
        }
        
        protected virtual void OnDestroy() => PlayerController.OnHitPlayer -= ResetCount;

        private void ResetCount()
        {
            StopAllCoroutines();
            Count = default;
            Text.text = default;
        }
    }
}
