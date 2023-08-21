using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.EntityPool.v1.Tags;
using Scellecs.Morpeh.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Scellecs.Morpeh.Addons.EntityPool.v1
{
    internal sealed class EntityPoolRegistry : IDisposable
    {
        private const int DefaultStackCapacity = 4;

        private static readonly IntHashMap<EntityPoolRegistry> WorldEntityPoolRegistry = new IntHashMap<EntityPoolRegistry>();

        internal FastList<Entity> EntitiesInUse { get; }
        
        private readonly World _world;
        private readonly Stash<PooledEntityTag> _pooledTagStash;

        private readonly Stack<Entity> _pooledEntities;

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
            _pooledTagStash = _world.GetStash<PooledEntityTag>();

            EntitiesInUse = new FastList<Entity>();
            _pooledEntities = new Stack<Entity>(DefaultStackCapacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Entity GetPooledEntity()
        {
            Entity entity;
            if (_pooledEntities.Count > 0)
                entity = _pooledEntities.Pop();
            else
                entity = CreatePooledEntity();

            EntitiesInUse.Add(entity);
            return entity;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void PoolEntity(Entity entity)
        {
            if (_pooledTagStash.Has(entity))
                EntitiesInUse.RemoveSwap(entity, out _);
            else
                _pooledTagStash.Add(entity);

            _pooledEntities.Push(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Entity CreatePooledEntity()
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
            EntitiesInUse.Clear();
        }

        public void Dispose()
        {
            ClearPooledEntities();
            WorldEntityPoolRegistry.Remove(_world.identifier, out _);
        }
    }
}
