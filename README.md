# MorpehAddons

[![Unity](https://img.shields.io/badge/Unity-2020.3%2B-black)](https://unity3d.com/pt/get-unity/download/archive) 
[![Morpeh](https://img.shields.io/badge/Morpeh-2023.1-3750c1)](https://github.com/scellecs/morpeh/) 
[![License](https://img.shields.io/badge/License-MIT-yellow)]()

Набор дополнительных инструментов для [Morpeh ECS](https://github.com/scellecs/morpeh/), добавляющих сахара для более "_feature-friendly_" синтаксиса фреймворка.

***NB: Поддерживается исключительно Morpeh 2023.1, который находится еще в разработке***

## Как поставить

Минимальная версия Unity - *Unity 2020.3.\* LTS* \
Также удостоверьтесь, что уже стоит *Morpeh 2023.1*

Откройте *Package Manager* и выберите *"Add package from git url..."* и затем вставьте следующую строку:
* `https://git.gameslab.store/nikita.nazarenko/morpehaddons.git?path=/Assets/Scripts/morpeh.addons`

## Из чего состоит пакет

### Systems

Набор чистых базовых классов систем, реализующих следующие интерфейсы Morpeh'а: `IInitializer`, `ISystem`, `IFixedSystem`, `ILateSystem` и `ICleanupSystem` по аналогии с встроенными в Morpeh SO [системами](https://github.com/scellecs/morpeh/tree/main/Unity/Systems). \
Используются сугубо для удобства, чтобы не прописывать для каждой очередной системы потенциально пустой метод `Dispose` и свойство `World`, которое используется везде.

### OneShot

Аналог OneFrame компонентов из [этого плагина](https://github.com/SH42913/morpeh.helpers) с новым названием, слегка переписанный с использованием нового API Morpeh 2023.1.

После регистрации "OneShot компонента" и добавлении его на какую-либо сущность, этот компонент будет сам автоматически очищен в конце кадра в специальной Cleanup системе (во время Late Update).

**API плагина:**
| Методы | Описание |
| ------ | ------ |
| `World.RegisterOneShot<TComponent>()` | Метод для регистрации OneShot компонента указанного типа |

Данный плагин _необходим_ для [Feature плагина](#feature).

_NB:_ Для использования OneShot плагина отдельно от остальных плагинов входящих в MorpehAddons достаточно внутри статического `[RuntimeInitializeOnLoadMethod]` метода вызвать следующее: `WorldExtensions.AddWorldPlugin(new OneShotPlugin());`.

### EntityPool



### Feature


