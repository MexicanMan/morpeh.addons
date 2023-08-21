using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.EntityPool.v2.Tags;
using Scellecs.Morpeh.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Scellecs.Morpeh.Addons.EntityPool.v2
{
    internal sealed class EntityPoolRegistry : IDisposable
    {
        private const int DefaultStackCapacity = 4;

        private static readonly IntHashMap<EntityPoolRegistry> WorldEntityPoolRegistry = new IntHashMap<EntityPoolRegistry>();

        private readonly World _world;
        private readonly Stash<PooledEntityTag> _pooledTagStash;
        private readonly Stash<FreeTag> _freeTagStash;

        private Stack<Entity> _pooledEntities;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InitializeEntityPool(World world)
        {
            var registry = new EntityPoolRegistry(world);
            WorldEntityPoolRegistry.Add(world.identifier, registry, out _);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static EntityPoolRegistry GetFor(World world)
        {
            return WorldEntityPoolRegistry.GetValueByKey(world.identifier);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Entity GetPooledEntity(World world)
        {
            return GetFor(world).GetPooledEntity();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void PoolEntity(World world, Entity entity)
        {
            GetFor(world).PoolEntity(entity);
        }

        private EntityPoolRegistry(World world)
        {
            _world = world;

            _pooledEntities = new Stack<Entity>(DefaultStackCapacity);
            _pooledTagStash = _world.GetStash<PooledEntityTag>();
            _freeTagStash = _world.GetStash<FreeTag>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Entity GetPooledEntity()
        {
            Entity entity;
            if (_pooledEntities.Count > 0)
            {
                entity = _pooledEntities.Pop();
                _freeTagStash.Remove(entity);
            }
            else
            {
                entity = CreatePooledEntityInUse();
            }

            return entity;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void PoolEntity(Entity entity)
        {
            if (!_pooledTagStash.Has(entity))
                _pooledTagStash.Add(entity);
            if (!_freeTagStash.Has(entity))
                _freeTagStash.Add(entity);

            _pooledEntities.Push(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Entity CreatePooledEntityInUse()
        {
            var entity = _world.CreateEntity();
            _pooledTagStash.Add(entity);

            return entity;
        }

        private void ClearPooledEntities()
        {
            foreach (var pooledEntity in _pooledEntities)
                pooledEntity.Dispose();

            _pooledEntities.Clear();
        }

        public void Dispose()
        {
            ClearPooledEntities();
            WorldEntityPoolRegistry.Remove(_world.identifier, out _);
        }
    }
}
