using Scellecs.Morpeh.Addons.Systems;

namespace Scellecs.Morpeh.Addons.EntityPool.v1.Systems
{
    internal sealed class ReturnToPoolSystem : CleanupSystem
    {
        private readonly EntityPoolRegistry _entityPool;

        public ReturnToPoolSystem(EntityPoolRegistry entityPool)
        {
            _entityPool = entityPool;
        }

        public override void OnAwake() { }

        public override void OnUpdate(float deltaTime)
        {
            _entityPool.PoolAllNotInUseEntities();
        }
    }
}
