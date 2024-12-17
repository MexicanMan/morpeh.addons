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
            return ref world.GetStash<T>().AddEvent();
        }

        [PublicAPI]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T AddEvent<T>(this Stash<T> eventStash) where T : struct, IComponent
        {
            var entity = eventStash.world.CreateEntity();
            return ref eventStash.Add(entity);
        }

        [PublicAPI]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetEvent<T>(this Stash<T> eventStash, in T eventComponent) where T : struct, IComponent
        {
            var entity = eventStash.world.CreateEntity();
            eventStash.Set(entity, eventComponent);
        }
    }
}
