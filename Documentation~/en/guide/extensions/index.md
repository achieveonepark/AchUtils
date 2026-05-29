# Extension Methods

`AchUtils.Extensions` contains the Unity runtime extension methods and small utility classes migrated from AchEngine.
All public APIs now use the `AchUtils` namespace, so add `using AchUtils;` in scripts that consume them.

## Package Info

| Item | Value |
|------|-------|
| Assembly | `AchUtils.Extensions` |
| Root namespace | `AchUtils` |
| Folder | `Runtime/Extensions` |
| Additional Unity packages | `com.unity.ugui`, `com.unity.nuget.newtonsoft-json` |

## Included Areas

### Collections

| Class | Purpose |
|---|---|
| `ArrayExt`, `ListExt` | Array and list helpers |
| `DictionaryExt`, `MultiDictionary<TKey,TValue>` | Dictionary helpers and multi-value keys |
| `IEnumerableExt`, `IListExt`, `LinqE` | LINQ helpers, shuffling, filtering, conversion |

### Unity Types

| Class | Purpose |
|---|---|
| `GameObjectExt`, `ComponentExt` | Component lookup, add/remove helpers, hierarchy traversal |
| `Vector2Ext`, `Vector3Ext`, `RectExt` | Coordinate, size, and bounds helpers |
| `ColorExt`, `ColorUtils` | Color conversion and hex helpers |
| `SpriteRendererExt`, `SpriteAtlasExt` | Sprite renderer and atlas helpers |

### UI

| Class | Purpose |
|---|---|
| `RectTransformExt` | UI layout coordinate and anchor helpers |
| `ImageExt`, `RawImageExt`, `GraphicExt`, `TextExt` | uGUI component helpers |
| `ButtonClickedEventExt` | Button click event wrappers |

### Primitive Types and Utilities

| Class | Purpose |
|---|---|
| `StringExt`, `StringParseExt`, `StringBuilderExt` | String checks, parsing, and composition |
| `IntExt`, `UintExt`, `FloatExt`, `BoolExt`, `ByteExt`, `EnumExt` | Primitive conversion and checks |
| `ActionExt`, `FuncExt`, `DelegateExt`, `UnityEventExt` | Callback and event helpers |
| `Selectable<T>`, `SelectableList<T>`, `SelectableBool` | Observable value wrappers |
| `SingleTask`, `MultiTask`, `StringAppender` | Repeated task and string assembly helpers |
| `PlayerPrefsExt`, `IJsonExt`, `IncomingWebhooks` | JSON persistence, serialization, and webhook sending |

## Usage

```csharp
using AchUtils;
using UnityEngine;

public class ExtensionSample : MonoBehaviour
{
    private void Awake()
    {
        var rb = gameObject.GetOrAddComponent<Rigidbody>();
        gameObject.SetActiveIfNotNull(true);

        var hp = new Selectable<int>(100);
        hp.mChanged += () => Debug.Log($"HP changed: {hp.Value}");
        hp.Value = 80;

        var skills = new MultiDictionary<string, string>();
        skills.Add("warrior", "Slash");
        skills.Add("warrior", "Block");
    }
}
```

## Migration

Code that previously used AchEngine only needs a namespace update.

```csharp
- using AchEngine;
+ using AchUtils;
```

The former `AchEngine.Extensions.ReflectionE` and `AchEngine.Extensions.ZciencE` namespaces are now `AchUtils.Extensions.ReflectionE` and `AchUtils.Extensions.ZciencE`.
