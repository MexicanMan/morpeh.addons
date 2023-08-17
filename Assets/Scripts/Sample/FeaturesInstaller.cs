using Assets.Scripts.Sample.PlayerInput;
using Scellecs.Morpeh.Addons.Feature;
using Scellecs.Morpeh.Addons.Feature.Unity;

namespace Assets.Scripts.Sample
{
    public class FeaturesInstaller : CodeFeaturesInstaller
    {
        protected override void InitializeShared()
        {
        }

        protected override UpdateFeature[] InitializeUpdateFeatures()
        {
            return new UpdateFeature[] 
            { 
                new PlayerInputFeature() 
            };
        }

        protected override FixedUpdateFeature[] InitializeFixedUpdateFeatures()
        {
            return new FixedUpdateFeature[] { };
        }

        protected override LateUpdateFeature[] InitializeLateUpdateFeatures()
        {
            return new LateUpdateFeature[] { };
        }
    }
}
