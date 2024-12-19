using Scellecs.Morpeh.Addons.Feature.ClearSystems;
using Unity.IL2CPP.CompilerServices;

namespace Scellecs.Morpeh.Addons.Feature
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public abstract class UpdateFeature : TypedFeature<ISystem>
    {
        public override void AddSystem(ISystem system)
        {
            if (system is IFixedSystem || system is ILateSystem || system is ICleanupSystem)
            {
            #if MORPEH_DEBUG
                MLogger.LogError($"Can not add the system \"{system.GetType()}\" " +
                    $"to the UpdateFeature \"{GetType()}\" because it is not exactly \"ISystem\".");
            #endif

                return;
            }

            base.AddSystem(system);
        }

        public override void Enable()
        {
            if (!enabled)
            {
                MoveSystemsList(featureSystemsGroup.disabledSystems, featureSystemsGroup.systems);

                enabled = true;
            }
        }

        public override void Disable()
        {
            if (enabled)
            {
                MoveSystemsList(featureSystemsGroup.systems, featureSystemsGroup.disabledSystems);

                enabled = false;
            }
        }

        private protected override void PreInitialize()
        {
            AddSystem(new ClearFromStashSystem(eventsStashes));
        }

        private protected override void PostInitialize()
        {
            AddSystem(new ClearFromStashSystem(requestsStashes));
        }
    }
}
