using Scellecs.Morpeh;

namespace Assets.Scripts.morpeh.feature
{
    public abstract class UpdateFeature : BaseFeature
    {
        public override BaseFeature AddSystem<ISystem>(ISystem system)
        {
            if (system is IFixedSystem || system is ILateSystem || system is ICleanupSystem)
            {
            #if MORPEH_DEBUG
                MLogger.LogError($"Can not add the system \"{system.GetType()}\" " +
                    $"to the UpdateFeature \"{GetType()}\" because it is not exactly \"ISystem\".");
            #endif

                return this;
            }

            return base.AddSystem(system);
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
    }
}
