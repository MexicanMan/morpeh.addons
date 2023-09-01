using Assets.Scripts.Sample.Spawn.Components.Events;
using Assets.Scripts.Sample.Spawn.Components.Requests;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Feature.Events;
using Scellecs.Morpeh.Addons.Systems;
using UnityEngine;

namespace Assets.Scripts.Sample.Logger.Systems
{
    internal sealed class LogSystem : UpdateSystem
    {
        private Filter _spawnedEvents;

        public override void OnAwake()
        {
            _spawnedEvents = World.Filter.With<SpawnedEvent>().Build();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var _ in _spawnedEvents)
            {
                Debug.Log("Spawned another cube!");
            }
        }
    }
}
