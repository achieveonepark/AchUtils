# Installation

## Install With Package Manager (Local)

Open your Unity project's `Packages/manifest.json` and add the package:

```json
{
  "dependencies": {
    "com.achieve.utils": "file:../Packages/AchUtils"
  }
}
```

You can also use **Package Manager -> Add package from disk** and select `Packages/AchUtils/package.json`.

## Install With Git URL

```json
{
  "dependencies": {
    "com.achieve.utils": "https://github.com/achieveonepark/AchUtils.git"
  }
}
```

## Verify

The package is installed when `AchUtils.Runtime` appears in Unity's Assembly Browser.

```csharp
using AchUtils.Tutorial;
using AchUtils.Buff;
// Use only the systems you need.
```

## Requirements

| Item | Requirement |
|------|-------------|
| Unity | 2021.3 LTS or newer |
| .NET | Standard 2.1 |
| Platforms | iOS, Android, PC, WebGL, Console |
| External packages | None |

## Import the Sample

Select the AchUtils package in Package Manager, then choose **Samples -> Basic Usage -> Import** to add the instance-based usage example to your project.

::: tip TextMeshPro
`DialogueStep` uses `UnityEngine.UI.Text` by default. If your project uses TMP, replace the `Text` component reference in `DialogueStep.cs` with `TextMeshProUGUI`.
:::
