using System;
using UnityEngine;

namespace Project.Player
{
    public class LaserModel : Model<LaserData>
    {
        public event Action<int> OnChangeCount;
        
        public int CurrentCountShotLaser { 
            get => _currentCountShotLaser;
            private set
            {
                _currentCountShotLaser = value;
                OnChangeCount?.Invoke(CurrentCountShotLaser);
            } 
        }
        
        public float LastShootTime { get; private set; }
        public float LastRechargeTime { get; private set; }

        private int _currentCountShotLaser;
        
        public LaserModel(LaserData data) : base(data){}

        public void Start()
        {
            CurrentCountShotLaser = Data.DefaultCountShotLaser;
        }
        
        public void Update()
        {
            LastRechargeTime += Time.deltaTime;
            LastShootTime += Time.deltaTime;
        }
        
        public void HandleShoot()
        {
            CurrentCountShotLaser -= 1;
            LastShootTime = 0f;
        }

        public void RestartValues()
        {
            CurrentCountShotLaser = Data.DefaultCountShotLaser;
            LastRechargeTime = 0;
        }
        
        public void IncreaseCountShotLaser()
        {
            LastRechargeTime = 0f;
            CurrentCountShotLaser += 1;
        }
    }
}
