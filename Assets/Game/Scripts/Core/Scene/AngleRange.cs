using System;
using UnityEngine;

namespace Project.System
{
    [Serializable]
    public class AngleRange
    {
        public float angle;
        
        public AngleRange(float angle)
        {
            this.angle = Mathf.Clamp(angle, 0, 360);
        }
    }
}