using JetBrains.Annotations;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Collections;
using System;

public abstract class BaseFeature<TFeatureSystem> : IDisposable where TFeatureSystem : class, ISystem
{
    private World _world;
    private SystemsGroup _featureSystemsGroup;

    internal void RegisterFeature(World world, int order)
    {
        _world = world;
        _featureSystemsGroup = _world.CreateSystemsGroup();

        Initialize();

        _world.AddSystemsGroup(order, _featureSystemsGroup);
    }

    protected abstract void Initialize();

    [PublicAPI]
    public BaseFeature<TFeatureSystem> AddInitializer(IInitializer initializer)
    {
        _featureSystemsGroup.AddInitializer(initializer);
        return this;
    }

    [PublicAPI]
    public BaseFeature<TFeatureSystem> AddSystem(TFeatureSystem system, bool enabled = true)
    {
        _featureSystemsGroup.AddSystem(system, enabled);
        return this;
    }

    public void Dispose()
    {
        if (_world != null && _featureSystemsGroup != null)
            _world.RemoveSystemsGroup(_featureSystemsGroup);
    }
}
