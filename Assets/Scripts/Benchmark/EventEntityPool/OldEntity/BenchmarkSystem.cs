using Assets.Scripts.Sample.PlayerInput.Components;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Feature.Events;
using Scellecs.Morpeh.Addons.Systems;
using UnityEngine;

namespace Assets.Scripts.Benchmark.EventEntityPool.OldEntity
{
    internal sealed class BenchmarkSystem : UpdateSystem
    {
        private readonly int _eventsPerFrame;

        private Filter _dontDestroyEntities;
        private Filter _events;

        public BenchmarkSystem(int eventsPerFrame)
        {
            _eventsPerFrame = eventsPerFrame;
        }

        public override void OnAwake()
        {
            _dontDestroyEntities = World.Filter
                .With<DontDestroyTag>()
                .Without<EcsEvent>()
                .Build();

            _events = World.Filter
                .With<EcsEvent>()
                .Build();
            
            for (int i = 0; i < _eventsPerFrame; i++)
            {
                var e = World.CreateEntity();
                e.AddComponent<DontDestroyTag>();
            }
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach (var e in _events)
                e.RemoveComponent<EcsEvent>();
            
            World.Commit();

            foreach (var e in _dontDestroyEntities)
                e.AddComponent<EcsEvent>();
        }
    }
}
