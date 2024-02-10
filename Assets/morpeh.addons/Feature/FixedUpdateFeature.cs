using Scellecs.Morpeh.Addons.Feature.ClearSystems;

namespace Scellecs.Morpeh.Addons.Feature
{
    public abstract class FixedUpdateFeature : TypedFeature<IFixedSystem>
    {
        public override void Enable()
        {
            if (!enabled)
            {
                MoveSystemsList(featureSystemsGroup.disabledFixedSystems, featureSystemsGroup.fixedSystems);

                enabled = true;
            }
        }

        public override void Disable()
        {
            if (enabled)
            {
                MoveSystemsList(featureSystemsGroup.fixedSystems, featureSystemsGroup.disabledFixedSystems);

                enabled = false;
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
