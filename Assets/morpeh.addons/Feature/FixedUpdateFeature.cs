using Scellecs.Morpeh.Addons.Feature.ClearSystems;
using Unity.IL2CPP.CompilerServices;

namespace Scellecs.Morpeh.Addons.Feature
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
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
