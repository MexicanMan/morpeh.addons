using JetBrains.Annotations;
using System.Runtime.CompilerServices;
using Scellecs.Morpeh.Addons.EntityPool;

namespace Scellecs.Morpeh.Addons.Feature.Events
{
    public static class EventsExtensions
    {
        [PublicAPI]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T CreateEventEntity<T>(this World world) where T : struct, IComponent
        {
            return ref world.GetPooledEntity().AddComponent<T>();
        }

        [PublicAPI]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T AddEvent<T>(this Stash<T> eventStash) where T : struct, IComponent
        {
            var pooledEntity = eventStash.world.GetPooledEntity();

            return ref eventStash.Add(pooledEntity);
        }

        [PublicAPI]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetEvent<T>(this Stash<T> eventStash, in T eventComponent) where T : struct, IComponent
        {
            var pooledEntity = eventStash.world.GetPooledEntity();
            eventStash.Set(pooledEntity, eventComponent);
        }
    }
}
