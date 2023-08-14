using Scellecs.Morpeh.Addons.Feature.ClearSystems;
using Scellecs.Morpeh;

namespace Scellecs.Morpeh.Addons.Feature
{
    public abstract class LateUpdateFeature : BaseFeature
    {
        public override BaseFeature AddSystem<ILateSystem>(ILateSystem system)
        {
            return base.AddSystem(system);
        }

        public override void Enable()
        {
            if (!enabled)
            {
                foreach (var lateSystem in featureSystemsGroup.disabledLateSystems)
                    featureSystemsGroup.EnableSystem(lateSystem);

                enabled = false;
            }
        }

        public override void Disable()
        {
            if (enabled)
            {
                foreach (var lateSystem in featureSystemsGroup.lateSystems)
                    featureSystemsGroup.DisableSystem(lateSystem);

                enabled = true;
            }
        }

        private protected override void PreInitialize()
        {
            AddSystem(new ClearFromStashLateSystem(eventsStashes));
        }

        private protected override void PostInitialize()
        {
            AddSystem(new ClearFromStashLateSystem(requestsStashes));
        }
    }
}
