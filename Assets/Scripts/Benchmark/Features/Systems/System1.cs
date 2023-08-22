﻿using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.Feature.Events;
using Scellecs.Morpeh.Addons.Systems;

namespace Assets.Scripts.Benchmark.Features
{
    internal sealed class System1 : UpdateSystem
    {
        private readonly int _eventsCount;
        private readonly bool _usePool;

        public System1(int eventsCount, bool usePool)
        {
            _eventsCount = eventsCount;
            _usePool = usePool;
        }

        public override void OnAwake() { }

        public override void OnUpdate(float deltaTime)
        {
            if (_usePool)
            {
                for (int i = 0; i < _eventsCount; i++)
                    World.CreateEventEntity<EcsEvent1>();
            }
            else
            {
                for (int i = 0; i < _eventsCount; i++)
                {
                    var e = World.CreateEntity();
                    e.AddComponent<EcsEvent1>();
                }
            }
        }
    }
}
