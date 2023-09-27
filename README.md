# MorpehAddons

[![Unity](https://img.shields.io/badge/Unity-2020.3%2B-black)](https://unity3d.com/pt/get-unity/download/archive) 
[![Morpeh](https://img.shields.io/badge/Morpeh-2023.1-3750c1)](https://github.com/scellecs/morpeh/) 
[![License](https://img.shields.io/badge/License-MIT-yellow)](LICENSE.md)

A set of additional tools for [Morpeh ECS](https://github.com/scellecs/morpeh/), adding sugar to implement a more "_feature-friendly_" framework syntax.

***NB: This package is not an official part of Morpeh ECS***

## Table of Contents

* [How to install](#how-to-install)
* [Package Description](#package-description)
    * [Systems](#systems)
    * [OneShot](#oneshot)
    * [Entity Pool](#entity-pool)
    * [Feature](#feature)
* [Examples](#examples)

## How to install

Unity minimum version - *Unity 2020.3.\* LTS* \
Also make sure you already have *Morpeh 2023.1* installed.

Open *Package Manager*, select *"Add package from git url..."* and then insert the following:
* `https://github.com/MexicanMan/morpeh.addons.git?path=/Assets/morpeh.addons`

## Package Description

### Systems

A set of pure base system classes implementing the following Morpeh interfaces: `IInitializer`, `ISystem`, `IFixedSystem`, `ILateSystem` and `ICleanupSystem` similar to the built-in Morpeh [SO systems](https://github.com/scellecs/morpeh/tree/main/Unity/Systems). \
They are used to avoid having to write for each system a potentially empty `Dispose()` method and `World` property, which is used everywhere.


### OneShot

An analogue of OneFrame components from [this plugin](https://github.com/SH42913/morpeh.helpers) with a new name, slightly rewritten using the new Morpeh 2023.1 API.

After registering a "OneShot component" and adding it to an entity, this component will be automatically cleaned itself at the end of the frame in a special Cleanup system (during Late Update).

This plugin is <ins>essential</ins> for [Feature plugin](#feature) work.

#### Plugin API 

| Methods | Description |
| ------- | ----------- |
| `World.RegisterOneShot<TComponent>()` | Registering a OneShot component of the specified type |

_NB:_ To use the OneShot plugin separately from the rest of the plugins included in MorpehAddons, just call the following inside the static `[RuntimeInitializeOnLoadMethod]` method: `WorldExtensions.AddWorldPlugin(new OneShotPlugin());`.


### Entity Pool

The plugin provides the ability to use an entity pool that, after deleting all its components (except for the reserved `PooledEntityTag` component tag), is returned to the free entity pool rather than simply deleted. \
It is important to understand that storing any references to pooled entities can be <ins>insecure</ins>, and the plugin is <ins>recommended</ins> to be used exclusively for frequently rotated entities with an unchanging number of components after creation. For example, for event or request entities, which usually don't live more than a frame and contain only one component.

This plugin is <ins>essential</ins> for [Feature plugin](#feature) work.

#### Plugin API

| Methods | Description |
| ------- | ----------- |
| `Entity World.GetPooledEntity()` | Retrieving an entity from a pool |
| `World.PoolEntity(Entity entity)` | Returning the entity back to the pool. It can also be used to add a new entity to the pool: The `PooledEntityTag` will be added to the new entity by itself. |

_NB:_ To use the Entity Pool plugin separately from the rest of the plugins included in MorpehAddons, just call the following inside the static `[RuntimeInitializeOnLoadMethod]` method: `WorldExtensions.AddWorldPlugin(new EntityPoolPlugin());`.


### Feature

The main plugin of the package, providing that very "*feature-friendly*" syntax for Morpeh. 

This plugin <ins>requires</ins> the [OneShot](#oneshot) and [Entity Pool](#entity-pool) plugins to work.

#### Features

**Feature** is some complete standalone game functionality, e.g. movement mechanic - `MovementFeature`. The size of a feature can vary and may include "sub-features", although in practice it is better to stick to atomic features as much as possible.

```csharp
internal class MovementFeature : UpdateFeature
{
    protected override void Initialize()
    {
        // You can also add initializers
        //AddInitializer(new SomeInitializer());

        AddSystem(new PlayerDirectionSystem());
        AddSystem(new MovementSystem());
    }
}
```

There are two main types of features within the package: 
* `TypedFeature<TFeatureSystem>` - "typed" by Update Queue features, which means that all systems will run exclusively in a given Update Queue. It is divided into 3 base classes - `UpdateFeature`, `FixedUpdateFeature` and `LateUpdateFeature`.
* `CombinedFeature` - a feature for systems running in a combined Update Queue. You can add systems working in a regular update (`ISystem`), FixedUpdate systems, and LateUpdate systems to this feature at the same time.

#### Requests and Events

In addition to the usual components and world data, the concepts of Requests and Events are also used to link features to each other:

* **Request** is a component that can be created from any feature to request some action inside the feature to which it belongs. The lifetime of such a component can be less than 1 cycle of the pipeline. Once it reaches the system that executes it in a feature, it will be processed and then automatically deleted. The deletion occurs after all systems in the feature have been processed, along with all other requests for performance reasons. \
All requests must be registered in their features in order to run their auto-delete at the end of the feature cycle:

```csharp
internal class SpawnFeature : UpdateFeature
{
    protected override void Initialize()
    {
        RegisterRequest<SpawnPlayer>();
        AddSystem(new PlayerSpawnSystem());
    }
}
```

* **Event** is a component that is created exclusively inside the feature it belongs to, to notify the game world and other systems/features of any events inside. The lifetime of such a component is usually exactly 1 pipeline cycle*. Once it reaches the feature that created it, it will be automatically deleted before any system of that feature is started. The deletion occurs at the same time as all other events in the feature for performance reasons. \
Similar to requests, all events must be registered in their features:

```csharp
internal class PlayerInputFeature : UpdateFeature
{
    protected override void Initialize()
    {
        AddSystem(new InputSystem());
        RegisterEvent<InputEvent>();
    }
}
```

The overall pipeline of the feature looks as follows:

![Feature Pipeline](Imgs/feature_pipeline.png)

\* - A different lifetime cycle can be set for Requests and Events, where deletion is started at the end of the current frame. For this purpose `RegisterRequest<TRequest>()` and `RegisterEvent<TEvent>()` methods have an optional `lifetime` parameter, which by default is equal to `EventLifetime.NotifyAllSystems`. To start the deletion of the set requests or events at the end of the frame you should enter `lifetime = EventLifetime.NotifyAllSystemsBelow`.

*NB:* For `CombinedFeature`, due to implementation specifics, methods `RegisterRequest()` and `RegisterEvent()` are combined into one - `RegisterOneShot<TOneShot>()`. Such components will always be deleted only at the end of the current frame.

The plugin also includes an additional API for creating requests and events on individual entities, which can be found below. These entities are not created from scratch each time, but are taken from a [pool](#entity-pool), so using them is much cheaper than creating via `World.CreateEntity()`. However, it is still not recommended to keep references to such entities and add additional components on them.

#### Plugin API

| Methods | Description |
| ------- | ----------- |
| `World.AddFeature(int order, BaseFeature feature, bool enabled = true)` | Adding a feature to the world |
| `World.RemoveFeature(BaseFeature feature)` | Removing a feature from the world |
| `TFeature World.GetFeature<TFeature>()` | Retrieve a feature of the specified type from the world if it was added to it. Otherwise `null` is returned |

* `TypedFeature<TFeatureSystem>` (`UpdateFeature`, `FixedUpdateFeature`, `LateUpdateFeature`): 

| Methods | Description |
| ------- | ----------- |
| `AddInitializer()` | Adding an initializer system to the feature (`IInitializer`) |
| `AddSystem()` | Adding a system to the feature that matches the Update Queue feature type |
| `RegisterRequest<TRequest>(EventLifetime lifetime = EventLifetime.NotifyAllSystems)` | Registration of a request of the specified type in a feature. By default, the specified request will be deleted after all feature systems are completed. The lifetime cycle can be modified so that the request is deleted at the end of the current frame (`EventLifetime.NotifyAllSystemsBelow`) |
| `RegisterEvent<TEvent>(EventLifetime lifetime = EventLifetime.NotifyAllSystems)` | Registration of an event of the specified type in a feature. By default, the specified event will be deleted before running the feature systems. The lifetime cycle can be modified so that the event is deleted at the end of the current frame (`EventLifetime.NotifyAllSystemsBelow`) |
| `Enable()` | Enabling a feature (e.g. for debugging) |
| `Disable()`| Disabling a feature (e.g. for debugging) |

* `CombinedFeature`:

| Methods | Description |
| ------- | ----------- |
| `AddInitializer()` | Adding an initializer system to the feature (`IInitializer`) |
| `AddSystem<TFeatureSystem>()` | Adding a system of the specified type to the feature (`ISystem` and its inheritors) |
| `RegisterOneShot<TOneShot>()` | Registration of a OneShot component of the specified type in a feature (for example, a request or an event). The specified component will be deleted at the end of the current frame |
| `Enable()` | Enabling a feature (e.g. for debugging) |
| `Disable()` | Disabling a feature (e.g. for debugging) |

* Creating event entities from the pool:

| Methods | Description |
| ------- | ----------- |
| `ref T World.CreateEventEntity<T>()` | Creating an entity from a pool and adding a component of the specified type |
| `ref T Stash<T>.AddEvent<T>()` | Adding an entity from the pool to the stash of a component of the specified type |
| `Stash<T>.SetEvent<T>(in T eventComponent)` | Setting the specified component type to an entity from the pool and adding it to the stash of this component |

_NB:_ Since the Feature plugin cannot be used separately from other plugins included in MorpehAddons, `MorpehAddons.Initialize()` must be called inside the static `[RuntimeInitializeOnLoadMethod]`. This initialization method will add all three plugins of the package to Morpeh: OneShot, Entity Pool and Feature. \
The second option is to use the `BaseFeaturesInstaller` integrated into the plugin, in which all the necessary initializations have already been done.

## Examples

* You can find a small sample [here](Assets/Sample)