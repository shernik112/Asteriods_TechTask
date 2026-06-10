using Project.Player;  
using Zenject;  
  
namespace Project.UI  
{  
    public class CounterStarter : NumberStarter  
    {  
        protected PlayerController PlayerController;  
  
        [Inject]  
        public void Construct(PlayerController playerController)  
        {  
            PlayerController = playerController;  
        }

        protected override void Awake()
        {
            base.Awake();
            PlayerController.DeathHandler.OnHitPlayer += ResetCount;  
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            PlayerController.DeathHandler.OnHitPlayer -= ResetCount;  
        }
  
        private void ResetCount() =>  
            Model.Reset();  
    }  
}

