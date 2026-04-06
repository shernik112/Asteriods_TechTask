using Project.Enemies;

namespace Project.Enemies
{
    public class FragmentAsteroidBehaviour : AsteroidBehaviour
    {
        protected override void HitBullet()
        {
            Deactivation();
        }
    }
}
