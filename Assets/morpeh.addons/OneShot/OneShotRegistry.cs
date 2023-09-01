using Scellecs.Morpeh;
using Scellecs.Morpeh.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Scellecs.Morpeh.Addons.OneShot
{
    internal sealed class OneShotRegistry : IDisposable
    {
        private static readonly IntHashMap<OneShotRegistry> WorldOneShotRegistry = new IntHashMap<OneShotRegistry>();

        private readonly World _world;
        private FastList<ICleanable> _registeredOneShots;

        public static OneShotRegistry GetFor(World world)
        {
            if (WorldOneShotRegistry.TryGetValue(world.identifier, out OneShotRegistry registry))
                return registry;

            registry = new OneShotRegistry(world);
            WorldOneShotRegistry.Add(world.identifier, registry, out _);
            return registry;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RegisterOneShot<TOneShot>(World world) where TOneShot : struct, IComponent
        {
            GetFor(world).RegisterOneShot<TOneShot>();
        }

        private OneShotRegistry(World world)
        {
            _world = world;

            _registeredOneShots = new FastList<ICleanable>();
        }

        private void RegisterOneShot<TOneShot>() where TOneShot : struct, IComponent
        {
            foreach (var registeredOneShot in _registeredOneShots)
                if (registeredOneShot.GetInnerType() == typeof(TOneShot))
                    return;

            _registeredOneShots.Add(new OneShotStash<TOneShot>(_world));
        }

        public void CleanOneShots()
        {
            foreach (var registeredOneShot in _registeredOneShots)
                registeredOneShot.Clean();
        }

        public void Dispose()
        {
            CleanOneShots();
            _registeredOneShots.Clear();
            WorldOneShotRegistry.Remove(_world.identifier, out _);
        }

        private interface ICleanable
        {
            void Clean();
            Type GetInnerType();
        }

        private sealed class OneShotStash<TOneShot> : ICleanable where TOneShot : struct, IComponent
        {
            private readonly Stash<TOneShot> _stash;

            public OneShotStash(World world)
            {
                _stash = world.GetStash<TOneShot>();
            }

            public void Clean()
            {
                _stash.RemoveAll();
            }

            public Type GetInnerType() => typeof(TOneShot);
        }
    }
}
