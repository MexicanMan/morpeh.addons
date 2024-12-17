using JetBrains.Annotations;
using Scellecs.Morpeh.Addons.Feature.Events;
using Scellecs.Morpeh.Addons.OneShot;
using Scellecs.Morpeh.Collections;
using System;

namespace Scellecs.Morpeh.Addons.Feature
{
    public abstract class TypedFeature<TFeatureSystem> : BaseFeature where TFeatureSystem : class, ISystem
    {
        private protected FastList<IStash> requestsStashes;
        private protected FastList<IStash> eventsStashes;

        [PublicAPI]
        public virtual void AddSystem(TFeatureSystem system)
        {
            featureSystemsGroup.AddSystem(system, enabled);
        }

        [PublicAPI]
        public void RegisterRequest<TRequest>(EventLifetime lifetime = EventLifetime.NotifyAllSystems)
            where TRequest : struct, IComponent
        {
            if (lifetime == EventLifetime.NotifyAllSystems)
                requestsStashes.Add(world.GetStash<TRequest>());
            else if (lifetime == EventLifetime.NotifyAllSystemsBelow)
                world.RegisterOneShot<TRequest>();
        }

        [PublicAPI]
        public void RegisterEvent<TEvent>(EventLifetime lifetime = EventLifetime.NotifyAllSystems)
            where TEvent : struct, IComponent
        {
            if (lifetime == EventLifetime.NotifyAllSystems)
                eventsStashes.Add(world.GetStash<TEvent>());
            else if (lifetime == EventLifetime.NotifyAllSystemsBelow)
                world.RegisterOneShot<TEvent>();
        }

        internal sealed override void Register(World world, int order, bool enabled = true)
        {
            this.world = world;
            featureSystemsGroup = world.CreateSystemsGroup();
            this.enabled = enabled;

            requestsStashes = new FastList<IStash>();
            eventsStashes = new FastList<IStash>();

            PreInitialize();
            Initialize();
            PostInitialize();

            world.AddSystemsGroup(order, featureSystemsGroup);
        }

        private protected abstract void PreInitialize();

        protected abstract void Initialize();

        private protected abstract void PostInitialize();

        public override void Dispose()
        {
            if (world != null && featureSystemsGroup != null)
            {
                requestsStashes.Clear();
                eventsStashes.Clear();  
                world.RemoveSystemsGroup(featureSystemsGroup);
            }
        }
    }
}
