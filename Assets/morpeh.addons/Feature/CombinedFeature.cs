using JetBrains.Annotations;
using Scellecs.Morpeh.Addons.OneShot;

namespace Scellecs.Morpeh.Addons.Feature
{
    public abstract class CombinedFeature : BaseFeature
    {
        [PublicAPI]
        public void AddSystem<TFeatureSystem>(TFeatureSystem system)
            where TFeatureSystem : class, ISystem
        {
            featureSystemsGroup.AddSystem(system, enabled);
        }

        [PublicAPI]
        public void RegisterOneShot<TOneShot>() where TOneShot : struct, IComponent
        {
            world.RegisterOneShot<TOneShot>();
        }

        public override void Enable()
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

        public override void Disable()
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

        internal sealed override void Register(World world, int order, bool enabled = true)
        {
            this.world = world;
            featureSystemsGroup = world.CreateSystemsGroup();
            this.enabled = enabled;

            Initialize();

            world.AddSystemsGroup(order, featureSystemsGroup);
        }

        protected abstract void Initialize();

        public override void Dispose()
        {
            if (world != null && featureSystemsGroup != null)
            {
                world.RemoveSystemsGroup(featureSystemsGroup);
            }
        }
    }
}
