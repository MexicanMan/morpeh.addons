using Assets.Scripts.Sample.Logger.Systems;
using Scellecs.Morpeh.Addons.Feature;

namespace Assets.Scripts.Sample.Logger
{
    public class LoggerFeature : UpdateFeature
    {
        protected override void Initialize()
        {
            AddSystem(new LogSystem());
        }
    }
}
