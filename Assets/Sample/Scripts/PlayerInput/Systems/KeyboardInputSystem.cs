using Assets.Scripts.Sample.Spawn.Components.Requests;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Feature.Events;
using Scellecs.Morpeh.Addons.Systems;
using UnityEngine;

namespace Assets.Scripts.Sample.PlayerInput.Systems
{
    internal sealed class KeyboardInputSystem : UpdateSystem
    {
        private const float SpawnPointRange = 10f;

        private Stash<SpawnRequest> _spawnRequestsStash;

        public override void OnAwake()
        {
            _spawnRequestsStash = World.GetStash<SpawnRequest>();
        }

        public override void OnUpdate(float deltaTime)
        {
            if (Input.GetKeyUp(KeyCode.S))
            {
                ref var rSpawn = ref _spawnRequestsStash.AddEvent();
                rSpawn.SpawnPosition = Random.onUnitSphere * SpawnPointRange;
            }
        }
    }
}
