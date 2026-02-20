using System.Collections;
using DigitalRuby.Tween;
using UnityEngine;
using Pixelplacement;

namespace Project.UI
{
    public class CountLaserShots : BaseCounter
    {
        public void UpdateValue(int count)
        {
            Count = count;
            Text.text = count.ToString();
        }
    }
}
