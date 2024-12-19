using System;
using JetBrains.Annotations;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Scellecs.Morpeh.Addons.Feature.Events
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public static class EventsExtensions
    {
        [PublicAPI]
        [Obsolete("[MORPEH] Use AddEvent() instead.")]
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
