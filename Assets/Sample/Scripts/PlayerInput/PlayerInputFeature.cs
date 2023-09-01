using Assets.Scripts.Sample.PlayerInput.Systems;
using Scellecs.Morpeh.Addons.Feature;

namespace Assets.Scripts.Sample.PlayerInput
{
    public class PlayerInputFeature : UpdateFeature
    {
        protected override void Initialize()
        {
            AddSystem(new KeyboardInputSystem());
        }
    }
}
