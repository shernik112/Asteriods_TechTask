using Project.Player;
using UnityEngine;

namespace Project.Enemies
{
    public abstract class EnemyModel : Model<EnemyDefinition>
    {
        public EnemyModel(EnemyDefinition data) : base(data){}

        public abstract void SetDefaultValues();
    }
}
