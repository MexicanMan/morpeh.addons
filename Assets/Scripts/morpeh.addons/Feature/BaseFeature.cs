using JetBrains.Annotations;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Collections;
using System;

namespace Scellecs.Morpeh.Addons.Feature
{
    public abstract class BaseFeature : IDisposable
    {
        private World _world;

        private protected SystemsGroup featureSystemsGroup;

        private protected FastList<Stash> requestsStashes;
        private protected FastList<Stash> eventsStashes;

        private protected bool enabled = true;

        [PublicAPI]
        public BaseFeature AddInitializer(IInitializer initializer)
        {
            featureSystemsGroup.AddInitializer(initializer);
            return this;
        }

        [PublicAPI]
        public virtual BaseFeature AddSystem<TFeatureSystem>(TFeatureSystem system)
            where TFeatureSystem : class, ISystem
        {
            featureSystemsGroup.AddSystem(system, enabled);
            return this;
        }

        [PublicAPI]
        public BaseFeature RegisterRequest<TRequest>() where TRequest : struct, IComponent
        {
            requestsStashes.Add(_world.GetStash<TRequest>());
            return this;
        }

        [PublicAPI]
        public BaseFeature RegisterEvent<TEvent>() where TEvent : struct, IComponent
        {
            eventsStashes.Add(_world.GetStash<TEvent>());
            return this;
        }

        [PublicAPI]
        public virtual void Enable()
        {
            if (!enabled)
            {
                foreach (var system in featureSystemsGroup.disabledSystems)
                    featureSystemsGroup.EnableSystem(system);
                foreach (var fixedSystem in featureSystemsGroup.disabledFixedSystems)
                    featureSystemsGroup.EnableSystem(fixedSystem);
                foreach (var lateSystem in featureSystemsGroup.disabledLateSystems)
                    featureSystemsGroup.EnableSystem(lateSystem);
                foreach (var cleanupSystem in featureSystemsGroup.disabledCleanupSystems)
                    featureSystemsGroup.EnableSystem(cleanupSystem);

                enabled = true;
            }
        }

        [PublicAPI]
        public virtual void Disable()
        {
            if (enabled)
            {
                foreach (var system in featureSystemsGroup.systems)
                    featureSystemsGroup.DisableSystem(system);
                foreach (var fixedSystem in featureSystemsGroup.fixedSystems)
                    featureSystemsGroup.DisableSystem(fixedSystem);
                foreach (var lateSystem in featureSystemsGroup.lateSystems)
                    featureSystemsGroup.DisableSystem(lateSystem);
                foreach (var cleanupSystem in featureSystemsGroup.cleanupSystems)
                    featureSystemsGroup.DisableSystem(cleanupSystem);

                enabled = false;
            }
        }

        internal void RegisterFeature(World world, int order, bool enabled = true)
        {
            _world = world;
            featureSystemsGroup = _world.CreateSystemsGroup();
            this.enabled = enabled;

            requestsStashes = new FastList<Stash>();
            eventsStashes = new FastList<Stash>();

            PreInitialize();
            Initialize();
            PostInitialize();

            _world.AddSystemsGroup(order, featureSystemsGroup);
        }


        protected abstract void Initialize();

        private protected abstract void PreInitialize();

        private protected virtual void PostInitialize()
        {

        }

        public void Dispose()
        {
            if (_world != null && featureSystemsGroup != null)
            {
                _world.RemoveSystemsGroup(featureSystemsGroup);
                requestsStashes.Clear();
                eventsStashes.Clear();
            }
        }
    }
}