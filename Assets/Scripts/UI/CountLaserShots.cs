using System.Collections;
using DigitalRuby.Tween;
using UnityEngine;
using Pixelplacement;

namespace Project.UI
{
    public class CountLaserShots : BaseCounter
    {
        [SerializeField] private Color changeColor = default;
        [SerializeField] private float durationChange = default;

        public void UpdateValue(int count)
        {
            Count = count;
            Text.text = count.ToString();
        }
    }
}
