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
                MoveSystemsList(featureSystemsGroup.disabledSystems, featureSystemsGroup.systems);
                MoveSystemsList(featureSystemsGroup.disabledFixedSystems, featureSystemsGroup.fixedSystems);
                MoveSystemsList(featureSystemsGroup.disabledLateSystems, featureSystemsGroup.lateSystems);
                MoveSystemsList(featureSystemsGroup.disabledCleanupSystems, featureSystemsGroup.cleanupSystems);

                enabled = true;
            }
        }

        public override void Disable()
        {
            if (enabled)
            {
                MoveSystemsList(featureSystemsGroup.systems, featureSystemsGroup.disabledSystems);
                MoveSystemsList(featureSystemsGroup.fixedSystems, featureSystemsGroup.disabledFixedSystems);
                MoveSystemsList(featureSystemsGroup.lateSystems, featureSystemsGroup.disabledLateSystems);
                MoveSystemsList(featureSystemsGroup.cleanupSystems, featureSystemsGroup.disabledCleanupSystems);

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
