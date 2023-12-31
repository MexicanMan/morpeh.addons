﻿using Scellecs.Morpeh.Addons.Feature.ClearSystems;

namespace Scellecs.Morpeh.Addons.Feature
{
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
                foreach (var system in featureSystemsGroup.disabledSystems)
                    featureSystemsGroup.EnableSystem(system);

                enabled = false;
            }
        }

        public override void Disable()
        {
            if (enabled)
            {
                foreach (var system in featureSystemsGroup.systems)
                    featureSystemsGroup.DisableSystem(system);

                enabled = true;
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
