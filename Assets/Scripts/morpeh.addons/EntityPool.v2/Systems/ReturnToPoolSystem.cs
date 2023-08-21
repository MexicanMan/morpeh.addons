using Scellecs.Morpeh.Addons.EntityPool.v2.Tags;
using Scellecs.Morpeh.Addons.Systems;

namespace Scellecs.Morpeh.Addons.EntityPool.v2.Systems
{
    internal sealed class ReturnToPoolSystem : CleanupSystem
    {
        private readonly EntityPoolRegistry _entityPool;

        private Filter _inUseEntities;

        public ReturnToPoolSystem(EntityPoolRegistry entityPool)
        {
            _entityPool = entityPool;
        }

        public override void OnAwake()
        {
            _inUseEntities = World.Filter.With<PooledEntityTag>().Without<FreeTag>().Build();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach(var inUseEntity in _inUseEntities)
            {
                if (inUseEntity.currentArchetypeLength == 1)
                    _entityPool.PoolEntity(inUseEntity);
            }
        }
    }
}
