using Pixelplacement;
using Project.Player;
using UnityEngine;

namespace Project
{
    [RequireComponent(typeof(SpriteRenderer),typeof(Collider2D))]
    public class LaserView : View<LaserData>
    {
        private Quaternion _initialLaserRotation;
        private SpriteRenderer _spriteRenderer;
        private Collider2D _collider;
        
        private void Awake()
        {
            _initialLaserRotation = transform.localRotation;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _collider = GetComponent<Collider2D>();
        }
        
        public void ChangeVisibility(bool visibility)
        {
            _spriteRenderer.enabled = visibility;
            _collider.enabled = visibility;
        }
        
        public void TurnLaser()
        {
            Tween.Rotate(transform, new Vector3(0, 0, -180),Space.Self, 
                Model.Data.DurationLaserShot, 0f, Tween.EaseInOut,Tween.LoopType.None,null,
                () =>
                {
                    ChangeVisibility(false);
                    transform.localRotation = _initialLaserRotation;
                });
        }
    }
}
