using Project.Enemies;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace Project
{
    public class AsteroidModel : EnemyModel
    {
        public AsteroidModel(EnemyDefinition data) : base(data){}

        public override void SetDefaultValues()
        {
        }
    }
}
