using JetBrains.Annotations;
using System.Runtime.CompilerServices;
using Scellecs.Morpeh.Addons.EntityPool;

namespace Scellecs.Morpeh.Addons.Feature.OneFrames
{
    public static class OneFrameExtensions
    {
        [PublicAPI]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T CreateOneFrame<T>(this World world) where T : struct, IComponent
        {
            return ref world.GetPooledEntity().AddComponent<T>();
        }

        [PublicAPI]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T AddOneFrame<T>(this Stash<T> oneFrameStash) where T : struct, IComponent
        {
            var pooledEntity = oneFrameStash.world.GetPooledEntity();

            return ref oneFrameStash.Add(pooledEntity);
        }

        [PublicAPI]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetOneFrame<T>(this Stash<T> oneFrameStash, in T oneFrameComponent) where T : struct, IComponent
        {
            var pooledEntity = oneFrameStash.world.GetPooledEntity();
            oneFrameStash.Set(pooledEntity, oneFrameComponent);
        }
    }
}
