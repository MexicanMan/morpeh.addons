using JetBrains.Annotations;
using System.Runtime.CompilerServices;

namespace Scellecs.Morpeh.Addons.EntityPool.v2
{
    public static class EntityPoolWorldExtensions
    {
        [PublicAPI]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Entity GetPooledEntity(this World world)
        {
            return EntityPoolRegistry.GetPooledEntity(world);
        }

        [PublicAPI]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PoolEntity(this World world, Entity entity)
        {
            EntityPoolRegistry.PoolEntity(world, entity);
        }
    }
}
