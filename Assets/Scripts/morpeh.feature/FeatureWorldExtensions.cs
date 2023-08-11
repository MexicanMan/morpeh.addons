using JetBrains.Annotations;
using Scellecs.Morpeh;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.morpeh.feature
{
    public static class FeatureWorldExtensions
    {
        [PublicAPI]
        public static void RegisterFeature<TFeature>(this World world, TFeature feature, bool enabled = true)
                where TFeature : BaseFeature<ISystem>
        {

        }
    }
}
