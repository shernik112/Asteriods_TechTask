using Project.Player;
using Zenject;

namespace Project.UI
{
    public class CounterStarter : NumberStarter
    {
        protected PlayerDeathHandler DeathHandler;

        [Inject]
        public void Construct(PlayerDeathHandler deathHandler)
        {
            DeathHandler = deathHandler;
        }

        protected override void Awake()
        {
            base.Awake();
            DeathHandler.OnHitPlayer += ResetCount;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            DeathHandler.OnHitPlayer -= ResetCount;
        }

        private void ResetCount() =>
            Model.Reset();
    }
}
