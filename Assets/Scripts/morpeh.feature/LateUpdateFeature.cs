using Scellecs.Morpeh;

namespace Assets.Scripts.morpeh.feature
{
    public abstract class LateUpdateFeature : BaseFeature
    {
        public override BaseFeature AddSystem<ILateSystem>(ILateSystem system)
        {
            return base.AddSystem(system);
        }

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
    }
}
