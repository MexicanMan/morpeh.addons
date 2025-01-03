# Morpeh.Addons

[![Unity](https://img.shields.io/badge/Unity-2020.3%2B-black)](https://unity3d.com/pt/get-unity/download/archive) 
[![Morpeh](https://img.shields.io/badge/Morpeh-2024.1-3750c1)](https://github.com/scellecs/morpeh/) 
[![License](https://img.shields.io/badge/License-MIT-yellow)](LICENSE.md)

Набор дополнительных инструментов для [Morpeh ECS](https://github.com/scellecs/morpeh/), добавляющих сахара для реализации более "_feature-friendly_" синтаксиса фреймворка.

***NB: Пакет не является официальной частью Morpeh ECS***

## Содержание

* [Как поставить](#как-поставить)
* [Из чего состоит пакет](#из-чего-состоит-пакет)
    * [Systems](#systems)
    * [OneShot](#oneshot)
    * [Feature](#feature)
* [Интеграция с VContainer](#интеграция-с-vcontainer)
* [Примеры](#примеры)

## Как поставить

Минимальная версия Unity - *Unity 2020.3.\* LTS* \
Также удостоверьтесь, что уже стоит *Morpeh 2024.1*

Откройте *Package Manager*, выберите *"Add package from git url..."* и затем вставьте следующую строку:
* `https://github.com/MexicanMan/morpeh.addons.git?path=/Assets/morpeh.addons`

## Из чего состоит пакет

### Systems

Набор чистых базовых классов систем, реализующих следующие интерфейсы Morpeh'а: `IInitializer`, `ISystem`, `IFixedSystem`, `ILateSystem` и `ICleanupSystem` по аналогии со встроенными в Morpeh [SO системами](https://github.com/scellecs/morpeh/tree/main/Unity/Systems). \
Используются сугубо для удобства, чтобы не прописывать для каждой очередной системы потенциально пустой метод `Dispose()` и свойство `World`, которое используется везде.


### OneShot

Аналог OneFrame компонентов из [этого плагина](https://github.com/SH42913/morpeh.helpers) с новым названием, слегка переписанный с использованием нового API Morpeh 2024.1.

После регистрации "OneShot компонента" и добавлении его на какую-либо сущность, этот компонент будет сам автоматически очищен в конце кадра в специальной Cleanup системе (во время Late Update).

Данный плагин <ins>необходим</ins> для работы [Feature плагина](#feature).

#### API плагина

| Методы | Описание |
| ------ | ------ |
| `World.RegisterOneShot<TComponent>()` | Регистрация OneShot компонента указанного типа |

_NB:_ Для использования OneShot плагина отдельно от остальных плагинов, входящих в MorpehAddons, достаточно внутри статического `[RuntimeInitializeOnLoadMethod]` метода вызвать следующее: `WorldExtensions.AddWorldPlugin(new OneShotPlugin());`.


### Feature

Основной плагин пакета, предоставляющий тот самый "*feature-friendly*" синтаксис для Morpeh'а. 

Данному плагину для работы <ins>требуется</ins> [OneShot](#oneshot) плагин.

#### Фичи

**Feature** (фича) - некоторая законченная обособленная игровая функциональность, например, механика передвижения - `MovementFeature`. Размер фичи может варьироваться и содержать в том числе "подфичи", хотя на практике лучше стараться придерживаться максимально атомарных фич.

```csharp
internal class MovementFeature : UpdateFeature
{
    protected override void Initialize()
    {
        // Можно также добавить инициализаторы
        //AddInitializer(new SomeInitializer());

        AddSystem(new PlayerDirectionSystem());
        AddSystem(new MovementSystem());
    }
}
```

В рамках пакета есть два основных вида фич: 
* `TypedFeature<TFeatureSystem>` - "типизированные" по Update Queue фичи, то есть все системы будут выполняться исключительно в заданном Update Queue. Делится на 3 базовых класса - `UpdateFeature`, `FixedUpdateFeature` и `LateUpdateFeature`.
* `CombinedFeature` - фича для систем, работающих в комбинированных Update Queue. В данную фичу можно одновременно добавлять как системы работающие в обычном апдейте (`ISystem`), так и FixedUpdate системы, и LateUpdate системы.

#### Реквесты и События

Для связи фич между собой, помимо обычных компонент и данных мира, также применяются понятия реквестов (Requests) и эвентов/событий (Events):

* **Реквест** - компонент, который может быть создан из любой фичи для запроса выполнить какое-либо действие внутри фичи, к которой принадлежит сам реквест. Время жизни такого компонента может быть меньше 1-го цикла пайплайна. Как только он дойдет до системы, которая выполняет его в фиче, то будет обработан и после этого автоматически удален. Удаление происходит после отработки всех систем фичи, вместе со всеми остальными реквестами данной фичи из соображений производительности. \
Все реквесты должны быть зарегистрированы в своих фичах для того, чтобы запускалось их автоудаление в конце цикла фичи:

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

* **Эвент** - компонент, который создается исключительно внутри фичи, к которой принадлежит, для оповещения игрового мира и других систем/фич о каких-либо событиях внутри. Время жизни такого компонента обычно составляет ровно 1 цикл пайплайна*. Как только он снова дойдет до фичи, создавшей его, то будет автоматически удален перед запуском какой-либо системы этой фичи. Удаление происходит одновременно со всеми остальными событиями данной фичи из соображений производительности. \
Аналогично реквестам, все события должны быть зарегистрированы в своих фичах:

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

Общий пайплайн фичи выглядит следующим образом:

![Feature Pipeline](Imgs/feature_pipeline.png)

\* - Для реквестов и эвентов может быть задан иной цикл жизни, когда уничтожение запускается в конце текущего фрейма. Для этого у методов `RegisterRequest<TRequest>()` и `RegisterEvent<TEvent>()` есть необязательный параметр `lifetime`, по умолчанию равный `EventLifetime.NotifyAllSystems`. Чтобы уничтожение заданных реквестов или событий запускалось в конце кадра необходимо указать `lifetime = EventLifetime.NotifyAllSystemsBelow`.

*NB:* Для `CombinedFeature`, в силу особенностей реализации, методы `RegisterRequest()` и `RegisterEvent()` объединены в один - `RegisterOneShot<TOneShot>()`. Удаляться такие компоненты соответственно всегда будут только в конце текущего кадра.

#### API плагина

| Методы | Описание |
| ------ | ------ |
| `World.AddFeature(int order, BaseFeature feature, bool enabled = true)` | Добавление фичи в мир |
| `World.RemoveFeature(BaseFeature feature)` | Удаление фичи из мира |
| `TFeature World.GetFeature<TFeature>()` | Получение фичи указанного типа из мира, если она была добавлена в него. Иначе вернется `null` |

* `TypedFeature<TFeatureSystem>` (`UpdateFeature`, `FixedUpdateFeature`, `LateUpdateFeature`): 

| Методы | Описание |
| ------ | ------ |
| `AddInitializer()` | Добавление в фичу системы-инициализатора (`IInitializer`) |
| `AddSystem()` | Добавление в фичу системы, совпадающей с типом Update Queue фичи |
| `RegisterRequest<TRequest>(EventLifetime lifetime = EventLifetime.NotifyAllSystems)` | Регистрация реквеста указанного типа в фиче. По умолчанию указанный реквест будет удален после отработки всех систем фичи. Цикл жизни может быть изменен таким образом, чтобы реквест удалялось в конце текущего кадра (`EventLifetime.NotifyAllSystemsBelow`) |
| `RegisterEvent<TEvent>(EventLifetime lifetime = EventLifetime.NotifyAllSystems)` | Регистрация события указанного типа в фиче. По умолчанию указанное событие будет удалено перед запуском систем фичи. Цикл жизни может быть изменен таким образом, чтобы событие удалялось в конце текущего кадра (`EventLifetime.NotifyAllSystemsBelow`) |
| `Enable()` | Включение фичи (например, для отладки) |
| `Disable()` | Выключение фичи (например, для отладки) |

* `CombinedFeature`:

| Методы | Описание |
| ------ | ------ |
| `AddInitializer()` | Добавление в фичу системы-инициализатора (`IInitializer`) |
| `AddSystem<TFeatureSystem>()` | Добавление в фичу системы указанного типа (`ISystem` и его наследники) |
| `RegisterOneShot<TOneShot>()` | Регистрация OneShot компонента указанного типа в фиче (например, реквеста или события). Указанный компонент будет удален в конце текущего кадра |
| `Enable()` | Включение фичи (например, для отладки) |
| `Disable()` | Выключение фичи (например, для отладки) |

* Создание отдельных сущностей-эвентов (синтаксический сахар, под капотом создается обычная сущность и ей навешивается указанный тип компонента):

| Методы | Описание |
| ------ | ------ |
| `ref T Stash<T>.AddEvent<T>()` | Создание сущности и добавление ее в стеш компонента указанного типа |
| `Stash<T>.SetEvent<T>(in T eventComponent)` | Установка компонента указанного типа с созданием сущности и добавлением в стеш этого компонента |

_NB:_ Так как Feature плагин не может использоваться отдельно от других плагинов, входящих в MorpehAddons, то для его иницилизации необходимо внутри статического `[RuntimeInitializeOnLoadMethod]` метода вызвать следующее: `MorpehAddons.Initialize()`. Данный метод инициализации добавит в Morpeh оба плагина пакета: OneShot и Feature. \
Второй вариант - использовать встроенный в плагин `BaseFeaturesInstaller`, в котором уже проведены все необходимые инициализации.

## Интеграция с VContainer
Если в проекте используется [VContainer](https://github.com/hadashiA/VContainer), то для создания фич можно использовать расширение `IObjectResolver.CreateFeature<TFeature>()` для инъекции зависимостей.

```csharp
protected override UpdateFeature[] InitializeUpdateFeatures()
{
    return new UpdateFeature[]
    {
        _container.CreateFeature<PlayerInputFeature>(),
        _container.CreateFeature<SpawnFeature>(),
        _container.CreateFeature<LoggerFeature>(),
    };
}
```

## Примеры

* Небольшой семпл может быть найден [здесь](Assets/Sample)