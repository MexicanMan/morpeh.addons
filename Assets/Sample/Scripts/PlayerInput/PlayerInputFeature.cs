using Assets.Scripts.Sample.PlayerInput.Systems;
using Scellecs.Morpeh.Addons.Feature;

namespace Assets.Scripts.Sample.PlayerInput
{
    internal class PlayerInputFeature : UpdateFeature
    {
        protected override void Initialize()
        {
            AddSystem(new KeyboardInputSystem());
        }
    }
}
