using Scellecs.Morpeh;

namespace Assets.Scripts.morpeh.feature
{
    public abstract class FixedUpdateFeature : BaseFeature
    {
        public override BaseFeature AddSystem<IFixedSystem>(IFixedSystem system)
        {
            return base.AddSystem(system);
        }

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
    }
}
