using Assets.Scripts.Sample.Spawn.Components.Events;
using Assets.Scripts.Sample.Spawn.Components.Requests;
using Assets.Scripts.Sample.Spawn.Systems;
using Scellecs.Morpeh.Addons.Feature;
using UnityEngine;

namespace Assets.Scripts.Sample.Spawn
{
    public class SpawnFeature : UpdateFeature
    {
        private readonly GameObject _cubePrefab;

        public SpawnFeature(GameObject cubePrefab)
        {
            _cubePrefab = cubePrefab;
        }

        protected override void Initialize()
        {
            RegisterRequest<SpawnRequest>();
            AddSystem(new SpawnCubeSystem(_cubePrefab));
            RegisterEvent<SpawnedEvent>();
        }
    }
}
