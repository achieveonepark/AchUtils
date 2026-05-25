# AchUtils Basic Usage Sample

This sample shows the intended instance-based usage pattern for AchUtils systems.

## How to Use

1. Import the sample from Unity Package Manager.
2. Create an empty GameObject in a scene.
3. Add only the scene components you want to test, such as `TutorialRunner`, `CameraDirector`, `SequenceRunner`, `Spawner`, and `RewindableObject`.
4. Add `AchUtilsSampleController`.
5. Assign the scene instances and ScriptableObject assets in the inspector.

The sample controller creates pure C# systems such as `BuffSystem`, `QuestSystem`, `SpawnSystem`, `TimeRewindSystem`, `ActionRecorder`, and `ActionPlayer` in code. This mirrors the package's runtime design: systems are independent instances, while Unity components are reserved for work that needs scene objects or coroutines.
