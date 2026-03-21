using Project.Player;
using UnityEngine;

namespace Project.Enemies
{
    public abstract class EnemyModel : Model<EnemyDefinition>
    {
        public abstract void SetDefaultValues();
    }
}
