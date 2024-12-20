using Scellecs.Morpeh.Addons.Feature.ClearSystems;
using Unity.IL2CPP.CompilerServices;

namespace Scellecs.Morpeh.Addons.Feature
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
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
