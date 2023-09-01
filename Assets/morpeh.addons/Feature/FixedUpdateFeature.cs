using Scellecs.Morpeh.Addons.Feature.ClearSystems;

namespace Scellecs.Morpeh.Addons.Feature
{
    public abstract class FixedUpdateFeature : TypedFeature<IFixedSystem>
    {
        public override void Enable()
        {
            if (!enabled)
            {
                foreach (var fixedSystem in featureSystemsGroup.disabledFixedSystems)
                    featureSystemsGroup.EnableSystem(fixedSystem);

                enabled = false;
            }
        }

        public override void Disable()
        {
            if (enabled)
            {
                foreach (var fixedSystem in featureSystemsGroup.fixedSystems)
                    featureSystemsGroup.DisableSystem(fixedSystem);

                enabled = true;
            }
        }

        private protected override void PreInitialize()
        {
            AddSystem(new ClearFromStashFixedSystem(eventsStashes));
        }

        private protected override void PostInitialize()
        {
            AddSystem(new ClearFromStashFixedSystem(requestsStashes));
        }
    }
}
