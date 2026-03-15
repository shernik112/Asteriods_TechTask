using Project.Enemies;
using UnityEngine;

namespace Project
{
    public class FragmentController : AsteroidController
    {
        protected override void HitBullet()
        {
            Deactivation();
        }
    }
}
