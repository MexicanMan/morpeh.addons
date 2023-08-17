using Scellecs.Morpeh.Addons.EntityPool.Tags;

namespace Scellecs.Morpeh.Addons.EntityPool.Systems
{
    internal sealed class ReturnToPoolSystem : ICleanupSystem
    {
        public World World { get; set; }

        private readonly EntityPoolRegistry _entityPool;

        private Filter _inUseEntities;

        public ReturnToPoolSystem(EntityPoolRegistry entityPool)
        {
            _entityPool = entityPool;
        }

        public void OnAwake()
        {
            _inUseEntities = World.Filter.With<PooledEntityTag>().Without<FreeTag>().Build();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach(var inUseEntity in _inUseEntities)
            {
                if (inUseEntity.currentArchetypeLength == 1)
                    _entityPool.PoolEntity(inUseEntity);
            }
        }

        public void Dispose()
        {
        }
    }
}
