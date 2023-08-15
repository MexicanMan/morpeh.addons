using Scellecs.Morpeh.Addons.Feature.ClearSystems;

namespace Scellecs.Morpeh.Addons.Feature
{
    public abstract class LateUpdateFeature : TypedFeature<ILateSystem>
    {
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
