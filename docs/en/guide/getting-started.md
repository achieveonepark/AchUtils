# What is AchUtils?

**AchUtils** is a collection of reusable systems for the repetitive parts of Unity game development.

Tutorials, condition graphs, red-dot badges, stat modifiers, and the other systems are designed to work independently, so you can install the package once and use only the pieces your game needs.

## Highlights

- **Independent instances** - Systems are not forced into global singletons unless the system itself needs scene-level coordination.
- **ScriptableObject-first data** - Data and runtime logic are separated so designers can adjust assets without code changes.
- **`[SerializeReference]` polymorphism** - Nodes, effects, and steps can be composed as inspector-editable data.
- **No external runtime dependencies** - The package only depends on Unity runtime APIs.

## Assembly

| Item | Value |
|------|-------|
| Package name | `com.achieveonepark.achutils` |
| Assembly | `AchieveOnePark.AchUtils.Runtime` |
| Root namespace | `AchieveOnePark.AchUtils` |
| Minimum Unity version | **2021.3 LTS** |

## Next Steps

- [Installation](/en/guide/installation)
- [First system: Tutorial](/en/systems/tutorial)
