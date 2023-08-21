using Scellecs.Morpeh.Addons.EntityPool.v1.Tags;
using Scellecs.Morpeh.Addons.Systems;

namespace Scellecs.Morpeh.Addons.EntityPool.v1.Systems
{
    internal sealed class ReturnToPoolSystem : CleanupSystem
    {
        private readonly EntityPoolRegistry _entityPool;

        private Stash<PooledEntityTag> _pooledTagStash;

        public ReturnToPoolSystem(EntityPoolRegistry entityPool)
        {
            _entityPool = entityPool;
        }

        public override void OnAwake()
        {
            _pooledTagStash = World.GetStash<PooledEntityTag>();
        }

        public override void OnUpdate(float deltaTime)
        {
            foreach(var entityInUse in _entityPool.EntitiesInUse)
            {
                if (entityInUse.currentArchetypeLength == 1)
                {
                #if MORPEH_DEBUG
                    if (!_pooledTagStash.Has(entityInUse))
                        MLogger.LogError($"You should never delete PooledTag from pool entity! " +
                            $"Entity id {entityInUse.ID.id}");
                #endif

                    _entityPool.PoolEntity(entityInUse);
                }
            }
        }
    }
}
