using Assets.Scripts.Sample.Spawn.Components.Events;
using Assets.Scripts.Sample.Spawn.Components.Requests;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Feature.Events;
using Scellecs.Morpeh.Addons.Systems;
using UnityEngine;

namespace Assets.Scripts.Sample.Spawn.Systems
{
    internal sealed class SpawnCubeSystem: UpdateSystem
    {
        private readonly GameObject _cubePrefab;

        private Filter _spawnRequests;

        private Stash<SpawnRequest> _spawnRequestsStash;
        private Stash<SpawnedEvent> _spawnedEventsStash;

        public SpawnCubeSystem(GameObject cubePrefab)
        {
            _cubePrefab = cubePrefab;
        }

        public override void OnAwake()
        {
            _spawnRequests = World.Filter.With<SpawnRequest>().Build();

            _spawnRequestsStash = World.GetStash<SpawnRequest>();
            _spawnedEventsStash = World.GetStash<SpawnedEvent>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var spawnRequest in _spawnRequests)
            {
                ref var rSpawn = ref _spawnRequestsStash.Get(spawnRequest);

                var spawnedCube = GameObject.Instantiate(_cubePrefab);
                spawnedCube.transform.SetPositionAndRotation(rSpawn.SpawnPosition, Quaternion.identity);

                _spawnedEventsStash.AddEvent();
            }
        }
    }
}
