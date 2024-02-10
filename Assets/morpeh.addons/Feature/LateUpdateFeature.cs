using Scellecs.Morpeh.Addons.Feature.ClearSystems;

namespace Scellecs.Morpeh.Addons.Feature
{
    public abstract class LateUpdateFeature : TypedFeature<ILateSystem>
    {
        public override void Enable()
        {
            if (!enabled)
            {
                MoveSystemsList(featureSystemsGroup.disabledLateSystems, featureSystemsGroup.lateSystems);

                enabled = true;
            }
        }

        public override void Disable()
        {
            if (enabled)
            {
                MoveSystemsList(featureSystemsGroup.lateSystems, featureSystemsGroup.disabledLateSystems);

                enabled = false;
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
