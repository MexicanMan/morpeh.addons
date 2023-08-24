using Scellecs.Morpeh;
using Scellecs.Morpeh.Addons.EntityPool.Tags;
using Scellecs.Morpeh.Collections;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Scellecs.Morpeh.Addons.EntityPool
{
    internal sealed class EntityPoolRegistry : IDisposable
    {
        private const int DefaultStackCapacity = 4;

        private static readonly IntHashMap<EntityPoolRegistry> WorldEntityPoolRegistry = new IntHashMap<EntityPoolRegistry>();

        private readonly World _world;
        private readonly Stash<PooledEntityTag> _pooledTagStash;

        private readonly FastList<Entity> _entitiesInUse;
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

            _entitiesInUse = new FastList<Entity>();
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

            _entitiesInUse.Add(entity);
            return entity;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void PoolEntity(Entity entity)
        {
            if (_pooledTagStash.Has(entity))
                _entitiesInUse.RemoveSwap(entity, out _);
            else
                _pooledTagStash.Add(entity);

            _pooledEntities.Push(entity);
        }

        public void PoolAllNotInUseEntities()
        {
            int i = 0;
            foreach (var entityInUse in _entitiesInUse)
            {
                if (entityInUse.currentArchetypeLength == 1)
                {
                #if MORPEH_DEBUG
                    if (!_pooledTagStash.Has(entityInUse))
                        MLogger.LogWarning($"You should never delete PooledTag from pool entity! " +
                            $"Entity id: {entityInUse.ID.id}.");
                #endif

                    _entitiesInUse.RemoveAtSwap(i, out FastList<Entity>.ResultSwap _);
                    _pooledEntities.Push(entityInUse);
                }
                else
                {
                    i++;
                }
            }
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
            _entitiesInUse.Clear();
        }

        public void Dispose()
        {
            ClearPooledEntities();
            WorldEntityPoolRegistry.Remove(_world.identifier, out _);
        }
    }
}
